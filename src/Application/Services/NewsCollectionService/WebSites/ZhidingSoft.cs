using HtmlAgilityPack;

namespace Application.Services.NewsCollectionService.WebSites;

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
        var result = new List<Rss>();
        var cookieContainer = new CookieContainer();
        using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
        using (var client = new HttpClient(handler))
        {
            //cookieContainer.Add(new Cookie("nsdr", @"""em=geethin%2540outlook.com|tkn=1|fnm=niltor|industry=Aerospace%252FDefense%2520Contractor|jobFunction=Applications|jobPosition=Manager""", "/", "infoworld.com"));
            //cookieContainer.Add(new Cookie("idg_uuid", "745fb237-4296-42e1-95ed-e06ebfa7c6ac", "/", "infoworld.com"));

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            // 转换编码
            var response = await client.GetByteArrayAsync(Url);
            var content = Encoding.GetEncoding("GB2312").GetString(response);

            if (!string.IsNullOrEmpty(content))
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(content);
                var rootNodes = doc.DocumentNode.SelectSingleNode(RootName);
                if (rootNodes != null)
                {
                    var itemsNodes = rootNodes.SelectNodes(ItemName);

                    itemsNodes.ToList().ForEach(node =>
                    {
                        var date = node.SelectSingleNode(PubDate)?.InnerText.Trim();
                        var catetory = node.SelectSingleNode(Category);

                        var news = new Rss
                        {
                            Categories = node.SelectSingleNode(Category)?.InnerText?.Trim(),
                            CreateTime = DateTime.TryParse(date, out var pubDate) ? pubDate : DateTime.Now,
                            Description = node.SelectSingleNode(Description)?.InnerText?.Trim(),
                            Title = node.SelectSingleNode(Title)?.InnerText?.Trim(),
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
