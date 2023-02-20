using Share.Models.SystemUserDtos;

namespace Application.IManager;
/// <summary>
/// 定义实体业务接口规范
/// </summary>
public interface ISystemUserManager : IDomainManager<SystemUser, SystemUserUpdateDto, SystemUserFilterDto, SystemUserItemDto>
{
    Task<bool> ChangePasswordAsync(SystemUser user, string password);

    // TODO: 定义业务方法
    Task<User?> GetInfoAsUserAsync(Guid id);
}
