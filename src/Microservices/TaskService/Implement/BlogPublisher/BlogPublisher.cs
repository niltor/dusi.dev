using TaskService.Implement.PostBlog;

namespace TaskService.Implement.BlogPublisher;

/// <summary>
/// blog publisher 
/// </summary>
public class BlogPublisher : IBlogPublisher
{
    public string BaseUrl { get; set; }

    public BlogPublisher(string baseUrl)
    {
        BaseUrl = baseUrl;
    }
    public virtual string GetToken(AuthOption option)
    {
        return string.Empty;
    }

    public virtual List<Catalog> GetCatalogs()
    {
        return default!;
    }

    public virtual bool AddCatalog(Catalog catalog)
    {
        return default;
    }

    public virtual bool AddBlog(Blog blog)
    {
        return default;
    }
}
