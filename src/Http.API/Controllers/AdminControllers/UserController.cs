
using Share.Models.UserDtos;

namespace Http.API.Controllers.AdminControllers;

/// <summary>
/// 用户账户
/// </summary>
public class UserController : RestControllerBase<UserManager>
{
    private readonly IConfiguration _config;
    private readonly SystemUserManager systemUserManager;

    public UserController(
        IUserContext user,
        ILogger<UserController> logger,
        UserManager manager,
        IConfiguration config,
        SystemUserManager systemUserManager) : base(manager, user, logger)
    {
        _config = config;
        this.systemUserManager = systemUserManager;
    }

    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("filter")]
    public async Task<ActionResult<PageList<UserItemDto>>> FilterAsync(UserFilterDto filter)
    {
        return await manager.FilterAsync(filter);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<User>> AddAsync(UserAddDto form)
    {
        if (await manager.FindAsync<User>(u => u.UserName == form.UserName) != null)
        {
            return Conflict("该用户名已被使用");
        }
        var entity = await manager.CreateNewEntityAsync(form);
        return await manager.AddAsync(entity);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"></param>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<User?>> UpdateAsync([FromRoute] Guid id, UserUpdateDto form)
    {
        var current = await manager.GetOwnedAsync(id);
        if (current == null) return NotFound();
        return await manager.UpdateAsync(current, form);
    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetDetailAsync([FromRoute] Guid id)
    {
        User? user = _user.IsAdmin
            ? await systemUserManager.GetInfoAsUserAsync(id)
            : await manager.FindAsync(id);
        return user == null ? NotFound() : user;
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    [HttpPut("password")]
    public async Task<ActionResult<bool>> ChangeMyPassword(string password)
    {
        var user = await manager.GetCurrentAsync(_user.UserId);
        if (user == null) return NotFound("未找到该用户");
        return await manager.ChangePasswordAsync(user, password);
    }

    /// <summary>
    /// ⚠删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // [ApiExplorerSettings(IgnoreApi = true)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<User?>> DeleteAsync([FromRoute] Guid id)
    {
        // 实现删除逻辑,注意删除权限
        var entity = await manager.GetOwnedAsync(id);
        if (entity == null) return NotFound();
        return await manager.DeleteAsync(entity, false);
    }
}