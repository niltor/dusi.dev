using Share.Models.SystemUserDtos;
namespace Http.API.Controllers.AdminControllers;

/// <summary>
/// 系统用户
/// </summary>
[Authorize(AppConst.Admin)]
public class SystemUserController : RestControllerBase<SystemUserManager>
{
    public SystemUserController(
        IUserContext user,
        ILogger<SystemUserController> logger,
        SystemUserManager manager
        ) : base(manager, user, logger)
    {
    }

    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("filter")]
    public async Task<ActionResult<PageList<SystemUserItemDto>>> FilterAsync(SystemUserFilterDto filter)
    {
        return await manager.FilterAsync(filter);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<SystemUser>> AddAsync(SystemUserAddDto form)
    {
        var entity = form.MapTo<SystemUserAddDto, SystemUser>();
        return await manager.AddAsync(entity);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"></param>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<SystemUser?>> UpdateAsync([FromRoute] Guid id, SystemUserUpdateDto form)
    {
        var current = await manager.GetCurrentAsync(id);
        if (current == null) return NotFound();
        return await manager.UpdateAsync(current, form);
    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<SystemUser?>> GetDetailAsync([FromRoute] Guid id)
    {
        var res = await manager.FindAsync(id);
        return res == null ? NotFound() : res;
    }


    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    [HttpPut("password")]
    public async Task<ActionResult<bool>> ChangeMyPassword(string password)
    {
        var user = await manager.GetCurrentAsync(_user.UserId!.Value);
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
    public async Task<ActionResult<SystemUser?>> DeleteAsync([FromRoute] Guid id)
    {
        var entity = await manager.GetCurrentAsync(id);
        if (entity == null) return NotFound();
        return await manager.DeleteAsync(entity);
    }
}