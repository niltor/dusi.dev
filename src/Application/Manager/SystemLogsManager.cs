using Application.IManager;
using Share.Models.SystemLogsDtos;

namespace Application.Manager;

public class SystemLogsManager : DomainManagerBase<SystemLogs, SystemLogsUpdateDto, SystemLogsFilterDto, SystemLogsItemDto>, ISystemLogsManager
{

    private readonly IUserContext _userContext;
    public SystemLogsManager(DataStoreContext storeContext, IUserContext userContext) : base(storeContext)
    {
        _userContext = userContext;
    }

    /// <summary>
    /// 创建待添加实体
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<SystemLogs> CreateNewEntityAsync(SystemLogsAddDto dto)
    {
        var entity = dto.MapTo<SystemLogsAddDto, SystemLogs>();
        // 构建实体
        return Task.FromResult(entity);
    }

    public override async Task<SystemLogs> UpdateAsync(SystemLogs entity, SystemLogsUpdateDto dto)
    {
        // 根据实际业务更新
        return await base.UpdateAsync(entity, dto);
    }

    public override async Task<PageList<SystemLogsItemDto>> FilterAsync(SystemLogsFilterDto filter)
    {
        // TODO:根据实际业务构建筛选条件
        // example: Queryable = Queryable.WhereNotNull(filter.field, q => q.field = filter.field);
        return await Query.FilterAsync<SystemLogsItemDto>(Queryable, filter.PageIndex, filter.PageSize);
    }

    /// <summary>
    /// 当前用户所拥有的对象
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<SystemLogs?> GetOwnedAsync(Guid id)
    {
        var query = Command.Db.Where(q => q.Id == id);
        // TODO:获取用户所属的对象
        // query = query.Where(q => q.User.Id == _userContext.UserId);
        return await query.FirstOrDefaultAsync();
    }
}
