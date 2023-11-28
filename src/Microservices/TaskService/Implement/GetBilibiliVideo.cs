using System.Text.Json.Nodes;
using Entity.CMS;

namespace TaskService.Implement;

/// <summary>
/// 最新更新视频
/// </summary>
public class GetBilibiliVideo
{
    public string Url { get; set; } = "https://api.bilibili.com/x/space/wbi/arc/search?mid=3493085797419759&pn=1&ps=25&index=1&order=pubdate";
    public GetBilibiliVideo()
    {
    }

    /// <summary>
    /// 获取最新视频
    /// </summary>
    /// <returns></returns>
    public async Task<ThirdVideo?> GetLatestVideoAsync()
    {
        // 模拟浏览器访问
        var cookieContainer = new CookieContainer();
        using var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
        using var http = new HttpClient(handler);
        http.DefaultRequestHeaders.TryAddWithoutValidation("cookie", "buvid3=3FB93364-EC1F-EDEC-63DC-FD702775D0CB95318infoc; b_nut=1680404295; _uuid=FBA24AD8-A136-A1062-BF10A-9AD6CD931085C25649infoc; buvid4=638BB2B2-9E77-4FF8-9564-A712A1D86A4495924-023040210-bsSZAilNIBHtwSRp8WXUWQ==; buvid_fp=38f8bc751a2e9beceb89f14432586289; sid=6u3snu9c; CURRENT_PID=81787b90-d102-11ed-b5b8-99a225c6ddf3; rpdid=|(J~kmu)|RJl0J'uY)|R|RJmu; PVID=1; CURRENT_FNVAL=16; b_lsid=35B78DBE_1874039A999");

        http.DefaultRequestHeaders.TryAddWithoutValidation("sec-ch-ua", "\"Microsoft Edge\";v=\"111\", \"Not(A:Brand\";v=\"8\", \"Chromium\";v=\"111\"");
        http.DefaultRequestHeaders.TryAddWithoutValidation("sec-ch-ua-mobile", " ?0");
        http.DefaultRequestHeaders.TryAddWithoutValidation("sec-ch-ua-platform", " \"Windows\"");
        http.DefaultRequestHeaders.TryAddWithoutValidation("sec-fetch-dest", " document");
        http.DefaultRequestHeaders.TryAddWithoutValidation("sec-fetch-mode", " navigate");
        http.DefaultRequestHeaders.TryAddWithoutValidation("sec-fetch-site", " none");
        http.DefaultRequestHeaders.TryAddWithoutValidation("sec-fetch-user", " ?1");
        http.DefaultRequestHeaders.TryAddWithoutValidation("upgrade-insecure-requests", " 1");
        http.DefaultRequestHeaders.TryAddWithoutValidation("user-agent", " Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/111.0.0.0 Safari/537.36 Edg/111.0.1661.62");


        var jsonObject = await http.GetFromJsonAsync<JsonObject>(Url);
        // data/list/vlist/[0]
        var data = jsonObject?["data"]?["list"]?["vlist"];

        var item = data?.AsArray().FirstOrDefault();
        if (item != null)
        {
            return new ThirdVideo
            {
                OriginalUrl = "https://www.bilibili.com/video/" + item["bvid"]?.GetValue<string>() ?? "",
                Title = item["title"]?.GetValue<string>() ?? "",
                ThumbnailUrl = item["pic"]?.GetValue<string>(),
                Description = item["description"]?.GetValue<string>(),
                Source = "bilibili",
                Identity = item["bvid"]?.GetValue<string>() ?? ""
            };
        }
        return default;
    }
}
