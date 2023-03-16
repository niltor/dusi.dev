using Dapr.Client;
using Grpc.BlogService;
using Share.Models.BlogDtos;

namespace Http.API.Controllers.AdminControllers;

[AllowAnonymous]
public class BlogController : RestControllerBase<IBlogManager>
{
    private readonly DaprClient dapr;

    public BlogController(
        IUserContext user,
        ILogger<BlogController> logger,
        IBlogManager manager,
        DaprClient dapr) : base(manager, user, logger)
    {
        this.dapr = dapr;
    }

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

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<string>> AddAsync(BlogAddDto form)
    {
        var reply = await dapr.InvokeMethodGrpcAsync<AddRequest, BlogReply>("cms", "add", new AddRequest { Name = "222" });

        return reply.Title;
    }

}