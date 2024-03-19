using Share.Models.UserDtos;

namespace Application.Manager;

public class UserManager(DataAccessContext<User> storeContext, IUserContext userContext, ILogger<UserManager> logger) : ManagerBase<User, UserUpdateDto, UserFilterDto, UserItemDto>(storeContext, logger)
{

    private readonly IUserContext _userContext = userContext;

    /// <summary>
    /// 创建待添加实体
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<User> CreateNewEntityAsync(UserAddDto dto)
    {
        User entity = dto.MapTo<UserAddDto, User>();
        entity.PasswordSalt = HashCrypto.BuildSalt();
        entity.PasswordHash = HashCrypto.GeneratePwd(dto.Password, entity.PasswordSalt);
        return Task.FromResult<User>(entity);
    }

    public override async Task<User> UpdateAsync(User entity, UserUpdateDto dto)
    {
        // 根据实际业务更新
        if (dto.Password != null)
        {
            entity.PasswordSalt = HashCrypto.BuildSalt();
            entity.PasswordHash = HashCrypto.GeneratePwd(dto.Password, entity.PasswordSalt);
        }
        return await base.UpdateAsync(entity, dto);
    }

    public override async Task<PageList<UserItemDto>> FilterAsync(UserFilterDto filter)
    {
        // TODO:根据实际业务构建筛选条件
        // if ... Queryable = ...
        return await Query.FilterAsync<UserItemDto>(Queryable, filter.PageIndex, filter.PageSize);
    }

    /// <summary>
    /// 当前用户所拥有的对象
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<User?> GetOwnedAsync(Guid id)
    {
        if (id != _userContext.UserId) return null;
        IQueryable<User> query = Command.Db.Where(q => q.Id == _userContext.UserId);
        return await query.FirstOrDefaultAsync();
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password">新密码</param>
    /// <returns></returns>
    public async Task<bool> ChangePasswordAsync(User user, string password)
    {
        string salt = HashCrypto.BuildSalt();
        user.PasswordHash = HashCrypto.GeneratePwd(password, salt);
        user.PasswordSalt = salt;
        _ = Command.Update(user);
        return await Command.SaveChangesAsync() > 0;
    }
}
