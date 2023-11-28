using Ater.MetaWeBlog;
using Ater.MetaWeBlog.Options;
using Microsoft.Extensions.Options;
using Share.Options;
using TaskService.Implement.PostBlog;

namespace TaskService.Implement.BlogPublisher;

/// <summary>
/// 博客园同步
/// </summary>
public class CnBlogPublisher : BlogPublisher
{
    private readonly ILogger<CnBlogPublisher> _logger;
    private readonly MetaWeblogOption option;

    public Client Client { get; set; }

    public CnBlogPublisher(IOptions<MetaWeblogOption> option, ILogger<CnBlogPublisher> logger) : base("")
    {
        this.option = option.Value;

        Client = new Client(new CnBlogsOption(
            this.option.BlogName,
            this.option.Username,
            this.option.PAT
        ));
        _logger = logger;
    }

    public override bool AddBlog(Blog blog)
    {
        try
        {
            var test = Client.GetUsersBlogs();
            var res = Client.NewPost(blog.Title, blog.Description, blog.Categories, DateTime.Now, true);
            _logger.LogInformation("post blog {title} {res}", blog.Title, res);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }

    }
}
