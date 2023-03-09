using TaskService.Implement.NewsCollector.RssFeeds;
namespace TaskService.Implement.NewsCollector;

public class RssHelper
{
    private readonly MicrosoftFeed microsoftFeed;
    private readonly OsChinaFeed osChinaFeed;
    private readonly InfoWorldFeed infoWorldFeed;

    public RssHelper(InfoWorldFeed infoWorldFeed, OsChinaFeed osChinaFeed, MicrosoftFeed microsoftFeed)
    {
        this.infoWorldFeed = infoWorldFeed;
        this.osChinaFeed = osChinaFeed;
        this.microsoftFeed = microsoftFeed;
    }
    public static bool IsContainKey(string[] strArray, string key)
    {
        foreach (var item in strArray)
        {
            if (key.Contains(item))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 获取所有rss内容
    /// </summary>
    /// <returns></returns>
    public async Task<List<Rss>> GetAllBlogsAsync()
    {
        var result = new List<Rss>();
        var list = await microsoftFeed.GetBlogsAsync();
        result.AddRange(list);

        list = await osChinaFeed.GetBlogsAsync(6);
        result.AddRange(list);

        list = await infoWorldFeed.GetBlogsAsync(5);
        result.AddRange(list);

        // 过滤旧数据
        result = result.Where(r => r.CreateTime >= DateTime.Now.AddDays(-1)).ToList();

        var blogs = new List<Rss>();
        foreach (var blog in result)
        {
            if (!blogs.Any(b => b.Title.Contains(blog.Title)))
            {
                blogs.Add(blog);
            }
        }
        return blogs;
    }
}
