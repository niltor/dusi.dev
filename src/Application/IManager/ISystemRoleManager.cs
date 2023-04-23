using Share.Models.SystemRoleDtos;

namespace Application.IManager;
/// <summary>
/// 定义实体业务接口规范
/// </summary>
public interface ISystemRoleManager : IDomainManager<SystemRole>
{
    Task<SystemRole?> GetCurrentAsync(Guid id, params string[] navigations);
    Task<SystemRole> AddAsync(SystemRole entity);
    Task<SystemRole> UpdateAsync(SystemRole entity, SystemRoleUpdateDto dto);
    Task<SystemRole?> FindAsync(Guid id);
    Task<TDto?> FindAsync<TDto>(Expression<Func<SystemRole, bool>>? whereExp) where TDto : class;
    Task<List<TDto>> ListAsync<TDto>(Expression<Func<SystemRole, bool>>? whereExp) where TDto : class;
    Task<PageList<SystemRoleItemDto>> FilterAsync(SystemRoleFilterDto filter);
    Task<SystemRole?> DeleteAsync(SystemRole entity, bool softDelete = true);
    Task<bool> ExistAsync(Guid id);
    // TODO: 定义业务方法
}
