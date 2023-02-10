using System.Text.Json;
using Core.Utils;
using Grpc.BlogService;
using Share.Models.BlogDtos;
using static Grpc.BlogService.Blog;
using Blog = Core.Entities.CMS.Blog;

namespace Http.API.Controllers;

[AllowAnonymous]
public class BlogController : RestControllerBase<IBlogManager>
{
    private readonly BlogClient _rpc;
    public BlogController(
        IUserContext user,
        ILogger<BlogController> logger,
        IBlogManager manager
,
        BlogClient rpc) : base(manager, user, logger)
    {
        _rpc = rpc;
    }

    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("filter")]
    public async Task<ActionResult<PageList<BlogItemDto>>> FilterAsync(BlogFilterDto filter)
    {
        var request = filter.MapTo<BlogFilterDto, FilterRequest>();
        var reply = await _rpc.FilterAsync(request);
        var  res = reply.MapTo<PageReply, PageList<BlogItemDto>>();
        return res;
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
        var reply = await _rpc.AddAsync(request);

        var json = JsonSerializer.Serialize(reply);
        var res = JsonSerializer.Deserialize<Blog>(json);
        return res;
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"></param>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Blog?>> UpdateAsync([FromRoute] Guid id, BlogUpdateDto form)
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
    public async Task<ActionResult<Blog?>> GetDetailAsync([FromRoute] Guid id)
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
    public async Task<ActionResult<Blog?>> DeleteAsync([FromRoute] Guid id)
    {
        // TODO:实现删除逻辑,注意删除权限
        var entity = await manager.GetOwnedAsync(id);
        if (entity == null) return NotFound();
        return Forbid();
        // return await manager.DeleteAsync(entity);
    }
}