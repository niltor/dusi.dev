using Share.Models.OpenSourceProductDtos;
namespace Http.API.Controllers.AdminControllers;

/// <summary>
/// 开源作品
/// </summary>
/// <see cref="Application.Manager.OpenSourceProductManager"/>
public class OpenSourceProductController : RestControllerBase<OpenSourceProductManager>
{

    public OpenSourceProductController(
        IUserContext user,
        ILogger<OpenSourceProductController> logger,
        OpenSourceProductManager manager
        ) : base(manager, user, logger)
    {

    }

    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("filter")]
    public async Task<ActionResult<PageList<OpenSourceProductItemDto>>> FilterAsync(OpenSourceProductFilterDto filter)
    {
        return await manager.FilterAsync(filter);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<OpenSourceProduct>> AddAsync(OpenSourceProductAddDto dto)
    {
        var entity = await manager.CreateNewEntityAsync(dto);
        return await manager.AddAsync(entity);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<OpenSourceProduct?>> UpdateAsync([FromRoute] Guid id, OpenSourceProductUpdateDto dto)
    {
        var current = await manager.GetOwnedAsync(id);
        if (current == null) return NotFound(ErrorMsg.NotFoundResource);
        return await manager.UpdateAsync(current, dto);
    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<OpenSourceProduct?>> GetDetailAsync([FromRoute] Guid id)
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
    public async Task<ActionResult<OpenSourceProduct?>> DeleteAsync([FromRoute] Guid id)
    {
        // 注意删除权限
        var entity = await manager.GetOwnedAsync(id);
        if (entity == null) return NotFound();
        // return Forbid();
        return await manager.DeleteAsync(entity);
    }
}