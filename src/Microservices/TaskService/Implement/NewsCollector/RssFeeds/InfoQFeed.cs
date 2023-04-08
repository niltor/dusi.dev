namespace TaskService.Implement.NewsCollector.RssFeeds;

public class InfoQFeed : BaseFeed
{
    public InfoQFeed(ILogger<InfoQFeed> logger) : base(logger, "InfoQ")
    {
        Urls = new string[]
        {
                "https://feed.infoq.com/",
        };
    }

    protected override string GetContent(string url) => base.GetContent(url);
}
