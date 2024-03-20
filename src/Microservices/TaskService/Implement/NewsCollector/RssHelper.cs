using TaskService.Implement.NewsCollector.RssFeeds;
namespace TaskService.Implement.NewsCollector;

public class RssHelper(InfoQFeed infoQFeed, OsChinaFeed osChinaFeed, MicrosoftFeed microsoftFeed)
{
    private readonly MicrosoftFeed microsoftFeed = microsoftFeed;
    private readonly OsChinaFeed osChinaFeed = osChinaFeed;
    private readonly InfoQFeed infoQFeed = infoQFeed;

    public static bool IsContainKey(string[] strArray, string key)
    {
        foreach (string item in strArray)
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
        List<Rss> result = [];
        List<Rss> list = await microsoftFeed.GetBlogsAsync();
        result.AddRange(list);

        list = await osChinaFeed.GetBlogsAsync(6);
        result.AddRange(list);

        list = await infoQFeed.GetBlogsAsync(5);
        result.AddRange(list);

        // 过滤旧数据
        result = result.Where(r => r.CreateTime >= DateTime.Now.AddDays(-1)).ToList();

        List<Rss> blogs = [];
        foreach (Rss blog in result)
        {
            if (!blogs.Any(b => b.Title.Contains(blog.Title)))
            {
                blogs.Add(blog);
            }
        }
        return blogs;
    }
}
