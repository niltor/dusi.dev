using Share.Models.SystemUserDtos;

namespace Application.IManager;
/// <summary>
/// 定义实体业务接口规范
/// </summary>
public interface ISystemUserManager : IDomainManager<SystemUser>
{
    Task<bool> ChangePasswordAsync(SystemUser user, string password);

    // TODO: 定义业务方法
    Task<User?> GetInfoAsUserAsync(Guid id);
    Task<SystemUser?> GetCurrentAsync(Guid id, params string[] navigations);
    Task<SystemUser> AddAsync(SystemUser entity);
    Task<SystemUser> UpdateAsync(SystemUser entity, SystemUserUpdateDto dto);
    Task<SystemUser?> FindAsync(Guid id);
    Task<TDto?> FindAsync<TDto>(Expression<Func<SystemUser, bool>>? whereExp) where TDto : class;
    Task<List<TDto>> ListAsync<TDto>(Expression<Func<SystemUser, bool>>? whereExp) where TDto : class;
    Task<PageList<SystemUserItemDto>> FilterAsync(SystemUserFilterDto filter);
    Task<SystemUser?> DeleteAsync(SystemUser entity, bool softDelete = true);
    Task<bool> ExistAsync(Guid id);
}
