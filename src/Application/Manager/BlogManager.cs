using Application.IManager;
using Core.Utils;
using Share.Models.BlogDtos;

namespace Application.Manager;

public class BlogManager : DomainManagerBase<Blog, BlogUpdateDto, BlogFilterDto, BlogItemDto>, IBlogManager
{

    private readonly IUserContext _userContext;
    public BlogManager(DataStoreContext storeContext, IUserContext userContext) : base(storeContext)
    {
        _userContext = userContext;
    }

    /// <summary>
    /// 创建待添加实体
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<Blog> CreateNewEntityAsync(BlogAddDto dto)
    {
        var entity = dto.MapTo<BlogAddDto, Blog>();
        // TODO:构建实体
        return Task.FromResult(entity);
    }

    public override async Task<Blog> UpdateAsync(Blog entity, BlogUpdateDto dto)
    {
        // TODO:根据实际业务更新
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
        if (!_userContext.IsAdmin)
        {
            // TODO:属于当前角色的对象
            // query = query.Where(q => q.User.Id == _userContext.UserId);
        }
        return await query.FirstOrDefaultAsync();
    }
}
