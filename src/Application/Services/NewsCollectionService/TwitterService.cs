using Microsoft.Extensions.Configuration;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace Application.Services.NewsCollectionService;

public class TwitterService
{
    private readonly string ConsumerKey = "";
    private readonly string ConsumerSecretKey = "";
    private readonly string AccessToken = "";
    private readonly string AccessSecretToken = "";
    private readonly TwitterClient client;
    private readonly ILogger _logger;
    private readonly IConfiguration _config;
    private readonly ContextBase _context;
    /// <summary>
    /// 需要关注的twitters
    /// </summary>
    public string[] Twitters { get; set; }
    public TwitterService(ILogger<TwitterService> logger, ContextBase context, IConfiguration configuration)
    {
        _logger = logger;
        _context = context;
        _config = configuration;

        var twitterConfig = _config.GetSection("Twitter");
        ConsumerKey = twitterConfig["ConsumerKey"];
        ConsumerKey = twitterConfig["ConsumerSecretKey"];
        ConsumerKey = twitterConfig["AccessToken"];
        ConsumerKey = twitterConfig["AccessSecretToken"];

        var userCredentials = new TwitterCredentials(ConsumerKey, ConsumerSecretKey, AccessToken, AccessSecretToken);
        client = new TwitterClient(userCredentials);
        Twitters = new string[] { "dotnet", "msdev", "googledevs", "BBCTech" };
    }

    public async Task StartAsync()
    {
        _logger.LogInformation("===Start=== Collect tweets");
        var tweets = await GetLastTweetsAsync();
        _logger.LogInformation("===Result=== collect tweets: " + tweets.Count);
        _logger.LogInformation("===Start=== Add tweets");
        await SaveTweetsAsync(tweets);
        _logger.LogInformation("===Result=== finish!");
    }

    public async Task SaveTweetsAsync(List<ThirdNews> list)
    {
        var result = new List<ThirdNews>(list);
        var news = await _context.ThirdNews.OrderByDescending(n => n.DatePublished)
            .Where(n => n.Type == NewsSource.Tweet)
            .Take(50).ToListAsync();

        foreach (var item in list)
        {
            if (news.Any(n => n.IdentityId == item.IdentityId
                || n.Title.GetSimilar(item.Title) >= 0.6))
            {
                result.Remove(item);
            }
        }

        if (result.Count > 0)
        {
            await _context.AddRangeAsync(result);
            await _context.SaveChangesAsync();
            _logger.LogInformation("===Start=== Add tweets " + result.Count);
        }
    }

    public async Task<List<ThirdNews>> GetLastTweetsAsync()
    {
        var messages = new List<ThirdNews>();
        foreach (var keywords in Twitters)
        {
            var resSets = await SearchTwitterAsync("from:" + keywords);
            // Content中带引用链接的处理,如 https://t.co/DBGGs8QFcJ
            var messageSets = resSets.Select(s => new ThirdNews
            {
                Provider = s.CreatedBy?.ScreenName,
                AuthorName = s.CreatedBy?.ScreenName,
                AuthorAvatar = s.CreatedBy.ProfileImageUrl400x400,
                Description = s.FullText,
                Title = s.FullText,
                CreatedTime = s.CreatedAt,
                Url = s.Url,
                Category = s.Hashtags.Count > 0 ? s.Hashtags.Select(t => t.Text).FirstOrDefault() : null,
                ThumbnailUrl = s.Media.FirstOrDefault()?.MediaURL,
                Type = NewsSource.Tweet
            }).ToList();
            messages.AddRange(messageSets);
        }
        // 去除重复的identityId
        messages.GroupBy(m => m.IdentityId).Select(g => g.First()).ToList();
        return messages;
    }


    public async Task<IEnumerable<ITweet>> SearchTwitterAsync(string keywords, int daysBefore = 1)
    {
        var searchParameter = new SearchTweetsParameters(keywords)
        {
            SearchType = SearchResultType.Mixed,
            PageSize = 3,
            // 最近一天的消息
            Since = DateTime.UtcNow.AddDays(-daysBefore)
        };
        //var matchingTweets = Search.SearchTweets(keywords);
        var tweets = await client.Search.SearchTweetsAsync(searchParameter);
        return tweets;
    }
}
