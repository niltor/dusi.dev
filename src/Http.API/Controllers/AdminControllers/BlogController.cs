using Share.Models.BlogDtos;

namespace Http.API.Controllers.AdminControllers;

/// <summary>
/// 博客
/// </summary>
public class BlogController(
    IUserContext user,
    ILogger<BlogController> logger,
    BlogManager manager) : RestControllerBase<BlogManager>(manager, user, logger)
{

    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("filter")]
    public async Task<ActionResult<PageList<BlogItemDto>>> FilterAsync(BlogFilterDto filter)
    {
        return await manager.FilterAsync(filter);
    }
}