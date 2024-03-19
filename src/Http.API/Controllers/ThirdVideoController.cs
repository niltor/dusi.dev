using Entity.CMS;
using Share.Models.ThirdVideoDtos;
namespace Http.API.Controllers;

/// <summary>
/// 三方视频
/// </summary>
public class ThirdVideoController : ClientControllerBase<ThirdVideoManager>
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
    [AllowAnonymous]
    public async Task<ActionResult<PageList<ThirdVideoItemDto>>> FilterAsync(ThirdVideoFilterDto filter)
    {
        filter.PageSize = 5;
        filter.PageIndex = 0;
        return await manager.FilterAsync(filter);
    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<ThirdVideo?>> GetDetailAsync([FromRoute] Guid id)
    {
        var res = await manager.FindAsync(id);
        return (res == null) ? NotFound() : res;
    }

}