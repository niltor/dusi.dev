namespace TaskService.Implement.NewsCollector.RssFeeds;

public class OsChinaFeed : BaseFeed
{
    public OsChinaFeed()
    {
        Urls = new string[]
        {
                "https://www.oschina.net/news/rss",
        };
    }

    protected override string GetContent(string url) => base.GetContent(url);
}
