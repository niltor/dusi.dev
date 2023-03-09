using Core.Entities.CMS;
using Core.Utils;

namespace TaskService.Implement.NewsCollector;

/// <summary>
/// 采集服务
/// </summary>
public class NewsCollector
{
    private readonly ILogger<NewsCollector> _logger;
    private readonly CommandDbContext _context;
    private readonly RssHelper rssHelper;
    public NewsCollector(ILogger<NewsCollector> logger, CommandDbContext context, RssHelper rssHelper)
    {
        _logger = logger;
        _context = context;
        this.rssHelper = rssHelper;
    }

    public async Task Start()
    {
        try
        {
            _logger.LogInformation("===Start=== Collect news");
            var list = await GetThirdNewsAsync();
            _logger.LogInformation("===Result=== collect news: {couont}", list.Count);
            _logger.LogInformation("===Start=== Add news");
            await AddThirdNewsAsync(list);
            _logger.LogInformation("===Result=== finish!");
        }
        catch (Exception ex)
        {
            _logger.LogError("{message}, {stackTrace}", ex.Message, ex.StackTrace);
        }

    }

    public async Task<List<ThirdNews>> GetThirdNewsAsync()
    {
        var news = await rssHelper.GetAllBlogsAsync();
        var result = new List<ThirdNews>();
        news.ForEach(news =>
        {
            var thirdNews = new ThirdNews
            {
                Category = news.Categories,
                Description = news.Description,
                Provider = news.Author,
                Title = news.Title,
                Url = news.Link,
                ThumbnailUrl = news.ThumbUrl,
                DatePublished = DateTime.SpecifyKind(news.CreateTime, DateTimeKind.Utc),
                Type = NewsSource.News
            };
            result.Add(thirdNews);
        });
        _logger.LogInformation("get all news!");
        return result;
    }

    public async Task AddThirdNewsAsync(List<ThirdNews> list)
    {
        var result = new List<ThirdNews>(list);
        var news = await _context.ThirdNews.OrderByDescending(n => n.DatePublished)
            .Where(n => n.Type == NewsSource.News)
            .Take(50).ToListAsync();

        _logger.LogInformation("today total news: {count}", list.Count);

        foreach (var item in list)
        {
            if (news.Any(n => n.Title.GetSimilar(item.Title) >= 0.6 || n.Title.Equals(item.Title)))
            {
                result.Remove(item);
            }
        }
        _logger.LogInformation("added news: {count}", result.Count);

        if (result.Count > 0)
        {
            await _context.AddRangeAsync(result);
            await _context.SaveChangesAsync();
        }
    }
}

