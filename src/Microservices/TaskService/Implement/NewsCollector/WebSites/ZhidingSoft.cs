
using HtmlAgilityPack;
using TaskService.Implement.NewsCollector;

namespace TaskService.Implement.NewsCollector.WebSites;

public class ZhidingSoft : BaseHtml
{
    public ZhidingSoft()
    {
        Url = "http://soft.zhiding.cn/";
        RootName = "//div[@class='information']";
        ItemName = "//div[@class='information_content ']";
        Description = ".//div[@class='right']/p[@class='right_content']";
        Title = ".//div[@class='right']/h3[@class='right_title']/a";
        PubDate = ".//div[@class='right']/div[@class='bottom']/div[@class='time']";
        Category = ".//div[@class='right']/div[@class='bottom']/div[@class='keyWord']/h3/a[1]";
        Link = ".//div[@class='right']/h3[@class='right_title']/a";
    }

    public async override Task<List<Rss>> GetListAsync(int number = 3)
    {
        List<Rss> result = new();
        CookieContainer cookieContainer = new();
        using (HttpClientHandler handler = new() { CookieContainer = cookieContainer })
        using (HttpClient client = new(handler))
        {
            //cookieContainer.Add(new Cookie("nsdr", @"""em=geethin%2540outlook.com|tkn=1|fnm=niltor|industry=Aerospace%252FDefense%2520Contractor|jobFunction=Applications|jobPosition=Manager""", "/", "infoworld.com"));
            //cookieContainer.Add(new Cookie("idg_uuid", "745fb237-4296-42e1-95ed-e06ebfa7c6ac", "/", "infoworld.com"));

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            // 转换编码
            byte[] response = await client.GetByteArrayAsync(Url);
            string content = Encoding.GetEncoding("GB2312").GetString(response);

            if (!string.IsNullOrEmpty(content))
            {
                HtmlDocument doc = new();
                doc.LoadHtml(content);
                HtmlNode rootNodes = doc.DocumentNode.SelectSingleNode(RootName);
                if (rootNodes != null)
                {
                    HtmlNodeCollection itemsNodes = rootNodes.SelectNodes(ItemName);

                    itemsNodes.ToList().ForEach(node =>
                    {
                        string? date = node.SelectSingleNode(PubDate)?.InnerText.Trim();
                        HtmlNode catetory = node.SelectSingleNode(Category);

                        Rss news = new()
                        {
                            Categories = node.SelectSingleNode(Category)?.InnerText?.Trim(),
                            CreateTime = DateTime.TryParse(date, out DateTime pubDate) ? pubDate : DateTime.Now,
                            Description = node.SelectSingleNode(Description)?.InnerText?.Trim(),
                            Title = node.SelectSingleNode(Title)?.InnerText?.Trim() ?? "",
                            Link = node.SelectSingleNode(Link)?.GetAttributeValue("href", "")
                        };

                        result.Add(news);
                    });
                }
            }
        }
        return result.Take(number).ToList();
    }

    public override string GetContent(string url) => base.GetContent(url);
}
