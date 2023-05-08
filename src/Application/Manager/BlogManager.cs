using System.IO;
using Application.Services;
using Core.Const;
using Microsoft.AspNetCore.Hosting;
using Microsoft.DocAsCode;
using Share.Models.BlogDtos;

namespace Application.Manager;

public class BlogManager : DomainManagerBase<Blog, BlogUpdateDto, BlogFilterDto, BlogItemDto>, IBlogManager
{
    private new readonly IUserContext _userContext;
    private readonly ICatalogManager _catalogManager;
    private readonly ITagsManager _tagsManager;
    private readonly IWebHostEnvironment _env;

    public BlogManager(DataStoreContext storeContext,
                       IUserContext userContext,
                       ICatalogManager catalogManager,
                       ITagsManager tagsManager,
                       IWebHostEnvironment env,
                       ILogger<BlogManager> logger) : base(storeContext, logger)
    {
        _userContext = userContext;
        _catalogManager = catalogManager;
        _tagsManager = tagsManager;
        _env = env;
    }

    /// <summary>
    /// 创建待添加实体
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<Blog> CreateNewEntityAsync(BlogAddDto dto)
    {
        Blog entity = dto.MapTo<BlogAddDto, Blog>();

        if (dto.TagIds != null && dto.TagIds.Any())
        {
            List<Tags> tags = await _tagsManager.Command.Db.Where(t => dto.TagIds.Contains(t.Id)).ToListAsync();
            if (tags != null)
            {
                entity.Tags = tags;
            }
        }

        var catalog = await _catalogManager.GetCurrentAsync(dto.CatalogId);
        entity.Catalog = catalog!;
        entity.UserId = _userContext.UserId!.Value;
        entity.Authors = _userContext.Username!;
        return entity;
    }

    public override async Task<Blog> AddAsync(Blog entity)
    {
        // 生成markdown文件
        var path = Path.Combine(_env.WebRootPath, "markdown", entity.Catalog.Name);

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var fileName = entity.Id.ToString() + ".md";
        await File.WriteAllTextAsync(Path.Combine(path, fileName), entity.Content);

        // 发送同步消息
        if (entity.IsPublic)
        {
            // 构建静态文件
            _ = Docset.Build(Path.Combine(_env.WebRootPath, "docfx.json"));
            _ = DaprFacade.PublishAsync(Const.PubNewBlog, entity.Id);
        }
        return await base.AddAsync(entity);
    }

    public override async Task<Blog?> FindAsync(Guid id)
    {
        Blog? res = await Queryable.Include(b => b.User)
            .Include(b => b.Tags)
            .Include(b => b.Catalog)
            .SingleOrDefaultAsync(b => b.Id == id);

        if (res == null) { return null; }
        //await DaprFacade.PublishAsync(Const.PubBlogView, id);
        return res;
    }

    /// <summary>
    /// 更新浏览量
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task UpdateViewCountAsync(Guid id)
    {
        try
        {
            // 统计浏览量,使用缓存
            // 缓存的blog id
            HashSet<Guid>? blogIds = await DaprFacade.GetStateAsync<HashSet<Guid>?>(AppConst.BlogViewCacheKey);
            // 初始添加
            int ttl = 7 * 24 * 60 * 60;
            if (blogIds == null)
            {
                HashSet<Guid> set = new()
            {
                id
            };
                await DaprFacade.SaveStateAsync(AppConst.BlogViewCacheKey, set, ttl);
            }
            else
            {
                // 新数据添加后更新到缓存
                if (blogIds.Add(id))
                {
                    await DaprFacade.SaveStateAsync(AppConst.BlogViewCacheKey, blogIds, ttl);
                }
            }
            // 数量存缓存
            int? count = await DaprFacade.GetStateAsync<int?>(AppConst.PrefixBlogView + id.ToString());
            if (count == null)
            {
                // 10分钟
                await DaprFacade.SaveStateAsync(AppConst.PrefixBlogView + id.ToString(), 1, 10 * 60);
            }
            else
            {
                count++;
                await DaprFacade.SaveStateAsync(AppConst.PrefixBlogView + id.ToString(), count, 10 * 60);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("update error:{message}", ex.Message + ex.StackTrace);
        }
    }

    public override async Task<Blog> UpdateAsync(Blog entity, BlogUpdateDto dto)
    {
        // 处理tagids
        if (dto.TagIds != null && dto.TagIds.Any())
        {
            List<Tags> tags = await _tagsManager.Command.Db.Where(t => dto.TagIds.Contains(t.Id)).ToListAsync();
            if (tags != null)
            {
                entity.Tags = tags;
            }
        }

        var path = Path.Combine(_env.WebRootPath, "markdown", entity.Catalog.Name);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var fileName = entity.Id.ToString() + ".md";
        await File.WriteAllTextAsync(Path.Combine(path, fileName), dto.Content);

        // 发布
        if (entity.IsPublic)
        {
            // 构建静态文件
            _ = Docset.Build(Path.Combine(_env.WebRootPath, "docfx.json"));
        }

        return await base.UpdateAsync(entity, dto);
    }

    public override async Task<PageList<BlogItemDto>> FilterAsync(BlogFilterDto filter)
    {
        // 根据实际业务构建筛选条件
        Queryable = Queryable.WhereNotNull(filter.Title, q => q.Title.Contains(filter.Title!))
            .WhereNotNull(filter.LanguageType, q => q.LanguageType == filter.LanguageType)
            .WhereNotNull(filter.Authors, q => q.Authors.Contains(filter.Authors!))
            .WhereNotNull(filter.IsPublic, q => q.IsPublic == filter.IsPublic)
            .WhereNotNull(filter.CatalogId, q => q.Catalog.Id == filter.CatalogId)
            .WhereNotNull(filter.BlogType, q => q.BlogType == filter.BlogType)
            .WhereNotNull(filter.Date, q => DateOnly.FromDateTime(q.CreatedTime.DateTime) == filter.Date);
        // tag筛选
        if (filter.Tag != null)
        {
            List<Guid>? blogIds = await GetBlogIdsByTagAsync(filter.Tag);
            Queryable = Queryable.WhereNotNull(blogIds, b => blogIds!.Contains(b.Id));
        }

        return await Query.FilterAsync<BlogItemDto>(Queryable, filter.PageIndex, filter.PageSize, filter.OrderBy);
    }

    public async Task<List<Guid>?> GetBlogIdsByTagAsync(string tag)
    {
        return await Query.Context.Tags.Where(t => t.Name == tag)
            .SelectMany(t => t.Blogs!)
            .Select(t => t.Id)
            .ToListAsync();
    }

    /// <summary>
    /// 获取分类信息
    /// </summary>
    /// <returns></returns>
    public List<EnumDictionary> GetTypes()
    {
        return EnumHelper.ToList(typeof(BlogType));
    }

    /// <summary>
    /// 当前用户所拥有的对象
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Blog?> GetOwnedAsync(Guid id)
    {
        IQueryable<Blog> query = Command.Db.Where(q => q.Id == id);

        query = query.Where(q => q.User.Id == _userContext.UserId)
            .Include(b => b.Tags)
            .Include(b => b.Catalog);

        return await query.FirstOrDefaultAsync();
    }
}
