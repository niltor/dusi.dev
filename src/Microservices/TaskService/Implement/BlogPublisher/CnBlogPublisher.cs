using Ater.MetaWeBlog;
using Ater.MetaWeBlog.Models;
using Ater.MetaWeBlog.Options;
using Google.Protobuf.WellKnownTypes;
using TaskService.Implement.PostBlog;

namespace TaskService.Implement.BlogPublisher;

public class CnBlogPublisher : BlogPublisher
{

    private readonly ILogger<CnBlogPublisher> _logger;

    public Client Client { get; set; }

    public CnBlogPublisher(string blogid, string username, string pat, ILogger<CnBlogPublisher> logger) : base("")
    {
        var option = new CnBlogsOption(blogid, username, pat);
        Client = new Client(option);
        _logger = logger;
    }


    public override bool AddBlog(Blog blog)
    {
        //Client.NewPost()
        var res = Client.NewPost(blog.Title, blog.Description, blog.Categories, DateTime.Now);
        _logger.LogInformation("post blog {title} {res}", blog.Title, res);
        return true;
    }
}
