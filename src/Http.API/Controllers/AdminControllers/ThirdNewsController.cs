using Share.Models.ThirdNewsDtos;
namespace Http.API.Controllers.AdminControllers;

/// <summary>
/// 资讯管理
/// </summary>
public class ThirdNewsController : RestControllerBase<ThirdNewsManager>
{
    public ThirdNewsController(
        IUserContext user,
        ILogger<ThirdNewsController> logger,
        ThirdNewsManager manager
        ) : base(manager, user, logger)
    {
    }

    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("filter")]
    public async Task<ActionResult<PageList<ThirdNewsItemDto>>> FilterAsync(ThirdNewsFilterDto filter)
    {
        filter.OrderBy = new Dictionary<string, bool>
        {
            ["CreatedTime"] = false,
            ["NewsType"] = true,
            ["TechType"] = true,
        };
        return await manager.FilterAsync(filter);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ThirdNews>> AddAsync(ThirdNewsAddDto form)
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
    public async Task<ActionResult<ThirdNews?>> UpdateAsync([FromRoute] Guid id, ThirdNewsUpdateDto form)
    {
        var current = await manager.GetOwnedAsync(id);
        if (current == null) return NotFound();
        return await manager.UpdateAsync(current, form);
    }


    /// <summary>
    /// 批量操作
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("batchUpdate")]
    public async Task<ActionResult<bool>> BatchUpdateAsync(ThirdNewsBatchUpdateDto dto)
    {
        return await manager.BatchUpdateAsync(dto);
    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ThirdNews?>> GetDetailAsync([FromRoute] Guid id)
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
    public async Task<ActionResult<ThirdNews?>> DeleteAsync([FromRoute] Guid id)
    {
        var entity = await manager.GetOwnedAsync(id);
        if (entity == null) return NotFound();
        return await manager.DeleteAsync(entity);
    }
}