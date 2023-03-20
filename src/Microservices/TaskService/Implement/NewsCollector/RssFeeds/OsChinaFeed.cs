namespace TaskService.Implement.NewsCollector.RssFeeds;

public class OsChinaFeed : BaseFeed
{
    public OsChinaFeed(ILogger<OsChinaFeed> logger) : base(logger, "OSC")
    {
        Urls = new string[]
        {
                "https://www.oschina.net/news/rss",
        };
    }

    protected override string GetContent(string url) => base.GetContent(url);
}
