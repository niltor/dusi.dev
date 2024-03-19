using System.Web;
using System.Xml.Linq;
using HtmlAgilityPack;

namespace TaskService.Implement.NewsCollector.RssFeeds;

public class MicrosoftFeed : BaseFeed
{
    public MicrosoftFeed(ILogger<MicrosoftFeed> logger) : base(logger, "MS")
    {
        Urls =
        [
                "https://devblogs.microsoft.com/dotnet/feed/",
                "https://devblogs.microsoft.com/typescript/feed/",
            //"https://blogs.microsoft.com/ai/feed/",
            //"https://devblogs.microsoft.com/aspnet/feed/",
            //"https://devblogs.microsoft.com/powershell/feed/",
            //"https://devblogs.microsoft.com/devops/feed/",
            //"https://devblogs.microsoft.com/visualstudio/feed/"
        ];
        Authorfilter = [ "MSFT", "Team", "Microsoft", "Visual", "Office", "Blog"
            ,"Jayme Singleton","Nish Anil","Phillip Carter","Olia Gavrysh","Daniel Roth","Cesar De la Torre"];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected override string? GetThumb(XElement? x)
    {
        if (x == null) return null;
        Uri guidLink = new(x.Value);
        string? guid = HttpUtility.ParseQueryString(x.Value).Get("p");
        string link = guidLink.AbsoluteUri.Replace(guidLink.Query, "");

        HtmlWeb hw = new();
        HtmlDocument doc = hw.LoadFromWebAsync(link).Result;
        string? content = doc.DocumentNode.SelectSingleNode($"//article[@id='post-{guid}']//noscript/img")?
            .GetAttributeValue("src", null);
        return content;
    }
}
