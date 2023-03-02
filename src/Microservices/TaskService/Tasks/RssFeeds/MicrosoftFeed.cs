using System.Web;
using System.Xml.Linq;
using HtmlAgilityPack;

namespace TaskService.Tasks.RssFeeds;

public class MicrosoftFeed : BaseFeed
{
    public MicrosoftFeed()
    {
        Urls = new string[]
        {
                "https://devblogs.microsoft.com/dotnet/feed/",
                "https://devblogs.microsoft.com/typescript/feed/",
            //"https://blogs.microsoft.com/ai/feed/",
            //"https://devblogs.microsoft.com/aspnet/feed/",
            //"https://devblogs.microsoft.com/powershell/feed/",
            //"https://devblogs.microsoft.com/devops/feed/",
            //"https://devblogs.microsoft.com/visualstudio/feed/"
        };
        Authorfilter = new string[] { "MSFT", "Team", "Microsoft", "Visual", "Office", "Blog"
            ,"Jayme Singleton","Nish Anil","Phillip Carter","Olia Gavrysh","Daniel Roth","Cesar De la Torre"};
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected override string GetThumb(XElement x)
    {
        var guidLink = new Uri(x.Value);
        var guid = HttpUtility.ParseQueryString(x.Value).Get("p");
        var link = guidLink.AbsoluteUri.Replace(guidLink.Query, "");

        var hw = new HtmlWeb();
        var doc = hw.LoadFromWebAsync(link).Result;
        var content = doc.DocumentNode.SelectSingleNode($"//article[@id='post-{guid}']//noscript/img")?
            .GetAttributeValue("src", null);
        return content;
    }
}
