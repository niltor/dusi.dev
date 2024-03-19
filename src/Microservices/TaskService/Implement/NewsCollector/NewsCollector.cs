using Entity.CMS;
using Ater.Web.Core.Utils;

namespace TaskService.Implement.NewsCollector;

/// <summary>
/// 采集服务
/// </summary>
public class NewsCollector(ILogger<NewsCollector> logger, CommandDbContext context, RssHelper rssHelper)
{
    private readonly ILogger<NewsCollector> _logger = logger;
    private readonly CommandDbContext _context = context;
    private readonly RssHelper rssHelper = rssHelper;

    public async Task Start()
    {
        try
        {
            _logger.LogInformation("⚡ Collect news");
            List<ThirdNews> list = await GetThirdNewsAsync();
            _logger.LogInformation("🔚 collect news: {couont}", list.Count);
            _logger.LogInformation("⚡ Add news");
            await AddThirdNewsAsync(list);
            _logger.LogInformation("🔚 finish!");
        }
        catch (Exception ex)
        {
            _logger.LogError("{message}, {stackTrace}", ex.Message, ex.StackTrace);
        }

    }

    public async Task<List<ThirdNews>> GetThirdNewsAsync()
    {
        List<Rss> news = await rssHelper.GetAllBlogsAsync();
        List<ThirdNews> result = new();
        news.ForEach(news =>
        {
            ThirdNews thirdNews = new()
            {
                Category = news.Categories,
                Description = news.Description,
                Provider = news.Provider ?? news.Author,
                AuthorName = news.Author,
                Title = news.Title,
                Url = news.Link,
                ThumbnailUrl = news.ThumbUrl,
                DatePublished = news.CreateTime.ToUniversalTime(),
                Type = NewsSource.News
            };
            result.Add(thirdNews);
        });
        _logger.LogInformation("get all news!");
        return result;
    }

    public async Task AddThirdNewsAsync(List<ThirdNews> list)
    {
        List<ThirdNews> result = new(list);
        var news = await _context.ThirdNews
            .IgnoreQueryFilters()
            .OrderByDescending(n => n.DatePublished)
            .Where(n => n.Type == NewsSource.News)
            .Take(50).ToListAsync();

        _logger.LogInformation("📰 Total news: {count}", list.Count);

        foreach (ThirdNews item in list)
        {
            if (news.Any(n => n.Title.GetSimilar(item.Title) >= 0.6 || n.Title.Equals(item.Title)))
            {
                result.Remove(item);
            }
        }
        _logger.LogInformation("🆕 added news: {count}", result.Count);

        if (result.Count > 0)
        {
            await _context.AddRangeAsync(result);
            await _context.SaveChangesAsync();
        }
    }
}

