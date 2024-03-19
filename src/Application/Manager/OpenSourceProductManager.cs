using Share.Models.OpenSourceProductDtos;

namespace Application.Manager;

public class OpenSourceProductManager(
    DataAccessContext<OpenSourceProduct> storeContext,
    ILogger<OpenSourceProductManager> logger,
    IUserContext userContext) : ManagerBase<OpenSourceProduct, OpenSourceProductUpdateDto, OpenSourceProductFilterDto, OpenSourceProductItemDto>(storeContext, logger)
{
    private readonly IUserContext _userContext = userContext;

    /// <summary>
    /// 创建待添加实体
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<OpenSourceProduct> CreateNewEntityAsync(OpenSourceProductAddDto dto)
    {
        OpenSourceProduct entity = dto.MapTo<OpenSourceProductAddDto, OpenSourceProduct>();
        // other required props
        return Task.FromResult(entity);
    }

    public override async Task<OpenSourceProduct> UpdateAsync(OpenSourceProduct entity, OpenSourceProductUpdateDto dto)
    {
        return await base.UpdateAsync(entity, dto);
    }

    public override async Task<PageList<OpenSourceProductItemDto>> FilterAsync(OpenSourceProductFilterDto filter)
    {
        Queryable = Queryable
            .WhereNotNull(filter.Title, q => q.Title == filter.Title)
            .WhereNotNull(filter.Tags, q => q.Tags != null && q.Tags.Contains(filter.Tags!));
        return await Query.FilterAsync<OpenSourceProductItemDto>(Queryable, filter.PageIndex, filter.PageSize, filter.OrderBy);
    }

    /// <summary>
    /// 当前用户所拥有的对象
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<OpenSourceProduct?> GetOwnedAsync(Guid id)
    {
        IQueryable<OpenSourceProduct> query = Command.Db.Where(q => q.Id == id);
        // 获取用户所属的对象
        // query = query.Where(q => q.User.Id == _userContext.UserId);
        return await query.FirstOrDefaultAsync();
    }

}
