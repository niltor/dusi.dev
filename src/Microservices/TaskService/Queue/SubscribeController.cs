using System.Security.Cryptography.Xml;
using Application;
using Application.IManager;
using Core.Const;
using Dapr;
using Microsoft.AspNetCore.Mvc;
using TaskService.Implement.PostBlog;

namespace TaskService.Queue;

/// <summary>
/// 订阅接口
/// </summary>
public class SubscribeController : ControllerBase
{
    private readonly IBlogPublisher blogPublisher;
    private readonly IBlogManager blogManager;

    public SubscribeController(
        IBlogPublisher blogPublisher,
        IBlogManager blogManager
        )
    {
        this.blogPublisher = blogPublisher;
        this.blogManager = blogManager;
    }

    [Topic(AppConst.DefaultPubSubName, Const.PubNewBlog)]
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
                Categories = new List<string> { data.Catalog.Name }
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
