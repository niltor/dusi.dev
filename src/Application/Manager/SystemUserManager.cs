using Share.Models.SystemUserDtos;

namespace Application.Manager;

public class SystemUserManager(DataAccessContext<SystemUser> storeContext, ILogger<SystemUserManager> logger) : ManagerBase<SystemUser, SystemUserUpdateDto, SystemUserFilterDto, SystemUserItemDto>(storeContext, logger)
{
    public override async Task<SystemUser> UpdateAsync(SystemUser entity, SystemUserUpdateDto dto)
    {
        // TODO:根据实际业务更新
        return await base.UpdateAsync(entity, dto);
    }

    public override async Task<PageList<SystemUserItemDto>> FilterAsync(SystemUserFilterDto filter)
    {
        // TODO:根据实际业务构建筛选条件
        // if ... Queryable = ...
        return await Query.FilterAsync<SystemUserItemDto>(Queryable);
    }


    /// <summary>
    /// 获取基本账号信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<User?> GetInfoAsUserAsync(Guid id)
    {
        SystemUser? sysUser = await FindAsync(id);

        return sysUser == null
            ? null
            : new User
            {
                UserName = sysUser.UserName,
                Id = sysUser.Id,
                Email = sysUser.Email,
                CreatedTime = sysUser.CreatedTime,
            };
    }
    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password">新密码</param>
    /// <returns></returns>
    public async Task<bool> ChangePasswordAsync(SystemUser user, string password)
    {
        string salt = HashCrypto.BuildSalt();
        user.PasswordHash = HashCrypto.GeneratePwd(password, salt);
        user.PasswordSalt = salt;
        _ = Command.Update(user);
        return await Command.SaveChangesAsync() > 0;
    }
}
