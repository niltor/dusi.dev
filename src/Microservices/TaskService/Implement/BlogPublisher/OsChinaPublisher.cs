using Ater.MetaWeBlog;
using Ater.MetaWeBlog.Options;
using TaskService.Implement.PostBlog;

namespace TaskService.Implement.BlogPublisher;

/// <summary>
/// 开源中国同步
/// </summary>
public class OsChinaPublisher : BlogPublisher
{
    private readonly ILogger<CnBlogPublisher> _logger;
    public Client Client { get; set; }

    public OsChinaPublisher(string blogid, string username, string pat, ILogger<CnBlogPublisher> logger) : base("")
    {
        var option = new CnBlogsOption(blogid, username, pat);
        Client = new Client(option);
        _logger = logger;
    }

    public override bool AddBlog(Blog blog)
    {
        var res = Client.NewPost(blog.Title, blog.Description, blog.Categories, DateTime.Now);
        _logger.LogInformation("post blog {title} {res}", blog.Title, res);
        return true;
    }
}

