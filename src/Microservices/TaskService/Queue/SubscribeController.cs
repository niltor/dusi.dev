using Application.Manager;
using Microsoft.AspNetCore.Mvc;
using TaskService.Implement.BlogPublisher;

namespace TaskService.Queue;

/// <summary>
/// 订阅接口
/// </summary>
public class SubscribeController : ControllerBase
{
    private readonly IBlogPublisher blogPublisher;
    private readonly BlogManager blogManager;

    public SubscribeController(
        IBlogPublisher blogPublisher,
        BlogManager blogManager
        )
    {
        this.blogPublisher = blogPublisher;
        this.blogManager = blogManager;
    }

    [HttpPost("/blog")]
    public async Task<ActionResult> NewBlog([FromBody] Guid id)
    {
        var data = await blogManager.Query.Db.Include(b => b.Catalog)
            .SingleOrDefaultAsync(b => b.Id == id);
        if (data != null)
        {
            var blog = new Blog
            {
                Description = data.Content,
                Title = data.Title,
                Categories = [data.Catalog.Name]
            };
            blogPublisher.AddBlog(blog);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}
