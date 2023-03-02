using TaskService.Tasks.RssFeeds;
using TaskService.Tasks.WebSites;

namespace TaskService.Tasks;

public class RssHelper
{
    public RssHelper()
    {
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
    public async static Task<List<Rss>> GetAllBlogsAsync()
    {
        var result = new List<Rss>();

        var zhidingWeb = new ZhidingSoft();
        var list = zhidingWeb.GetListAsync(5).Result;
        result.AddRange(list);

        var msFeed = new MicrosoftFeed();
        list = await msFeed.GetBlogsAsync();
        result.AddRange(list);

        var osChinaFeed = new OsChinaFeed();
        list = await osChinaFeed.GetBlogsAsync(6);
        result.AddRange(list);

        var infoWorldFeed = new InfoWorldFeed();
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
