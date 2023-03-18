using Core.Const;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Share.Models.BlogDtos;

namespace Application.Manager;

public class BlogManager : DomainManagerBase<Blog, BlogUpdateDto, BlogFilterDto, BlogItemDto>, IBlogManager
{
    private readonly IUserContext _userContext;
    private readonly ICatalogManager _catalogManager;
    private readonly ITagsManager _tagsManager;

    public BlogManager(DataStoreContext storeContext, IUserContext userContext, ICatalogManager catalogManager, ITagsManager tagsManager) : base(storeContext)
    {
        _userContext = userContext;
        _catalogManager = catalogManager;
        _tagsManager = tagsManager;
    }

    /// <summary>
    /// 创建待添加实体
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<Blog> CreateNewEntityAsync(BlogAddDto dto)
    {
        var entity = dto.MapTo<BlogAddDto, Blog>();
        var user = await _userContext.GetUserAsync() ?? throw new Exception(ErrorMsg.NotFoundUser);
        var catalog = await _catalogManager.GetCurrentAsync(dto.CatalogId) ?? throw new Exception("不存在的目录");

        if (dto.TagIds != null && dto.TagIds.Any())
        {
            var tags = await _tagsManager.Command.Db.Where(t => dto.TagIds.Contains(t.Id)).ToListAsync();

            if (tags != null)
            {
                entity.Tags = tags;
            }
        }

        entity.User = user;
        entity.Catalog = catalog;
        entity.Authors = user.UserName;
        return entity;
    }

    public override Task<Blog?> FindAsync(Guid id)
    {
        return Queryable.Include(b => b.User)
            .Include(b => b.Tags)
            .Include(b => b.Catalog)
            .SingleOrDefaultAsync(b => b.Id == id);
    }

    public override async Task<Blog> UpdateAsync(Blog entity, BlogUpdateDto dto)
    {
        if (dto.CatalogId != null)
        {
            // 修改了所属目录
            if (entity.Catalog.Id != dto.CatalogId)
            {
                var catalog = await _catalogManager.GetCurrentAsync(dto.CatalogId.Value) ?? throw new Exception("不存在的目录");
                entity.Catalog = catalog;
            }
        }
        // 处理tagids
        if (dto.TagIds != null && dto.TagIds.Any())
        {
            entity.Tags = null;

            var tags = await _tagsManager.Command.Db.Where(t => dto.TagIds.Contains(t.Id)).ToListAsync();
            if (tags != null)
            {
                entity.Tags = tags;
            }
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
            .WhereNotNull(filter.Date, q => DateOnly.FromDateTime(q.CreatedTime.DateTime) == filter.Date);
        // tag筛选
        if (filter.Tag != null)
        {
            var blogIds = await GetBlogIdsByTagAsync(filter.Tag);
            Queryable = Queryable.WhereNotNull(blogIds, b => blogIds!.Contains(b.Id));
        }

        return await Query.FilterAsync<BlogItemDto>(Queryable, filter.PageIndex, filter.PageSize);
    }

    public async Task<List<Guid>?> GetBlogIdsByTagAsync(string tag)
    {
        return await Query.Context.Tags.Where(t => t.Name == tag)
            .Where(t => t.User.Id == _userContext.UserId)
            .SelectMany(t => t.Blogs!)
            .Select(t => t.Id)
            .ToListAsync();
    }

    /// <summary>
    /// 当前用户所拥有的对象
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Blog?> GetOwnedAsync(Guid id)
    {
        var query = Command.Db.Where(q => q.Id == id);

        query = query.Where(q => q.User.Id == _userContext.UserId)
            .Include(b => b.Tags)
            .Include(b => b.Catalog);

        return await query.FirstOrDefaultAsync();
    }
}
