using Application.Implement;
using Application.IManager;
using Share.Models.OpenSourceProductDtos;

namespace Application.Manager;

public class OpenSourceProductManager : DomainManagerBase<OpenSourceProduct, OpenSourceProductUpdateDto, OpenSourceProductFilterDto, OpenSourceProductItemDto>, IOpenSourceProductManager
{

    public OpenSourceProductManager(
        DataStoreContext storeContext,
        IUserContext userContext) : base(storeContext)
    {

        _userContext = userContext;
    }

    /// <summary>
    /// 创建待添加实体
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<OpenSourceProduct> CreateNewEntityAsync(OpenSourceProductAddDto dto)
    {
        var entity = dto.MapTo<OpenSourceProductAddDto, OpenSourceProduct>();
        // other required props
        return Task.FromResult(entity);
    }

    public override async Task<OpenSourceProduct> UpdateAsync(OpenSourceProduct entity, OpenSourceProductUpdateDto dto)
    {
        return await base.UpdateAsync(entity, dto);
    }

    public override async Task<PageList<OpenSourceProductItemDto>> FilterAsync(OpenSourceProductFilterDto filter)
    {
        // https://github.com/AterDev/ater.web/blob/56542e5653ee795855705e43482e64df0ee8383d/templates/apistd/src/Core/Utils/Extensions.cs#L82
        Queryable = Queryable
            .WhereNotNull(filter.Title, q => q.Title == filter.Title);
        // TODO: custom filter conditions
        return await Query.FilterAsync<OpenSourceProductItemDto>(Queryable, filter.PageIndex, filter.PageSize, filter.OrderBy);
    }

    /// <summary>
    /// 当前用户所拥有的对象
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<OpenSourceProduct?> GetOwnedAsync(Guid id)
    {
        var query = Command.Db.Where(q => q.Id == id);
        // 获取用户所属的对象
        // query = query.Where(q => q.User.Id == _userContext.UserId);
        return await query.FirstOrDefaultAsync();
    }

}
