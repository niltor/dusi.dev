using TaskService.Implement.NewsCollector;

namespace TaskService.Implement.NewsCollector.WebSites;

public class BaseHtml
{
    protected string? Url { get; set; }
    protected string[]? HtmlTagFilter { get; set; }
    /// <summary>
    /// root结点 
    /// </summary>
    protected string? RootName { get; set; }
    /// <summary>
    /// xml item 名称
    /// </summary>
    protected string? ItemName { get; set; }
    protected string? Author { get; set; }
    protected string? Description { get; set; }
    protected string? Title { get; set; }
    protected string? PubDate { get; set; }
    protected string? Content { get; set; }
    protected string? Category { get; set; }
    public string? Link { get; set; }
    protected HttpClient httpClient = new();

    public async virtual Task<List<Rss>> GetListAsync(int number = 3)
    {
        var result = new List<Rss>();
        return await Task.FromResult(result);
    }

    public virtual string GetContent(string url) => "";
}
