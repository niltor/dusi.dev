using Share.Models.ThirdVideoDtos;
namespace Http.API.Controllers.AdminControllers;

/// <summary>
/// 三方视频
/// </summary>
public class ThirdVideoController : RestControllerBase<ThirdVideoManager>
{
    public ThirdVideoController(
        IUserContext user,
        ILogger<ThirdVideoController> logger,
        ThirdVideoManager manager
        ) : base(manager, user, logger)
    {
    }

    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("filter")]
    public async Task<ActionResult<PageList<ThirdVideoItemDto>>> FilterAsync(ThirdVideoFilterDto filter)
    {
        return await manager.FilterAsync(filter);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ThirdVideo>> AddAsync(ThirdVideoAddDto form)
    {
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
    public async Task<ActionResult<ThirdVideo?>> UpdateAsync([FromRoute] Guid id, ThirdVideoUpdateDto form)
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
    public async Task<ActionResult<ThirdVideo?>> GetDetailAsync([FromRoute] Guid id)
    {
        var res = await manager.FindAsync(id);
        return (res == null) ? NotFound() : res;
    }

    /// <summary>
    /// ⚠删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // [ApiExplorerSettings(IgnoreApi = true)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ThirdVideo?>> DeleteAsync([FromRoute] Guid id)
    {
        // TODO:实现删除逻辑,注意删除权限
        var entity = await manager.GetOwnedAsync(id);
        if (entity == null) return NotFound();
        return Forbid();
        // return await manager.DeleteAsync(entity);
    }
}