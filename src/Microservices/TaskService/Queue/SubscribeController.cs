using Application.Manager;
using Microsoft.AspNetCore.Mvc;
using TaskService.Implement.BlogPublisher;

namespace TaskService.Queue;

/// <summary>
/// 订阅接口
/// </summary>
public class SubscribeController(
    IBlogPublisher blogPublisher,
    BlogManager blogManager
        ) : ControllerBase
{
    private readonly IBlogPublisher blogPublisher = blogPublisher;
    private readonly BlogManager blogManager = blogManager;

    [HttpPost("/blog")]
    public async Task<ActionResult> NewBlog([FromBody] Guid id)
    {
        Entity.CMS.Blog? data = await blogManager.Query.Db.Include(b => b.Catalog)
            .SingleOrDefaultAsync(b => b.Id == id);
        if (data != null)
        {
            Blog blog = new()
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
