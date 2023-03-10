using System.Text.Json;
using Core.Entities.CMS;
using Dapr.Client;
using Grpc.BlogService;
using Share.Models.BlogDtos;
namespace Http.API.Controllers;

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
    public async Task<ActionResult<Core.Entities.CMS.Blog>> AddAsync(BlogAddDto form)
    {
        var request = form.MapTo<BlogAddDto, AddRequest>();
        var reply = await dapr.InvokeMethodGrpcAsync<AddRequest, BlogReply>("cms", "Add", request);
        var res = reply.MapTo<BlogReply, Core.Entities.CMS.Blog>();
        return res;
    }

    [HttpGet("filter")]
    public async Task<ActionResult> Filter()
    {
        var request = new FilterRequest
        {
            PageIndex = 1,
            PageSize = 12
        };
        var reply = await dapr.InvokeMethodGrpcAsync<FilterRequest, PageReply>("cms", "Filter", request);

        return Ok();
    }

    ///// <summary>
    ///// 更新
    ///// </summary>
    ///// <param name="id"></param>
    ///// <param name="form"></param>
    ///// <returns></returns>
    //[HttpPut("{id}")]
    //public async Task<ActionResult<Blog?>> UpdateAsync([FromRoute] Guid id, BlogUpdateDto form)
    //{
    //    var current = await manager.GetOwnedAsync(id);
    //    if (current == null) return NotFound();
    //    return await manager.UpdateAsync(current, form);
    //}

    ///// <summary>
    ///// 详情
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //[HttpGet("{id}")]
    //public async Task<ActionResult<Blog?>> GetDetailAsync([FromRoute] Guid id)
    //{
    //    var res = await manager.FindAsync(id);
    //    return (res == null) ? NotFound() : res;
    //}

    ///// <summary>
    ///// ⚠删除
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //// [ApiExplorerSettings(IgnoreApi = true)]
    //[HttpDelete("{id}")]
    //public async Task<ActionResult<Blog?>> DeleteAsync([FromRoute] Guid id)
    //{
    //    // 实现删除逻辑,注意删除权限
    //    var entity = await manager.GetOwnedAsync(id);
    //    if (entity == null) return NotFound();
    //    return await manager.DeleteAsync(entity);
    //}
}