using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Share.Models.AuthDtos;

namespace Dusi.Manage.Client;


public class ManagerClient : IManagerClient
{
    private string BaseUrl { get; set; } = "";
    public string? AccessToken { get; set; }
    public HttpClient Http { get; set; }
    public JsonSerializerOptions JsonSerializerOptions { get; set; }
    public ErrorResult? ErrorMsg { get; set; } = null;

    public ManagerClient(AuthOption option)
    {
        Http = new HttpClient()
        {
            BaseAddress = new Uri(BaseUrl),
        };
        JsonSerializerOptions = new JsonSerializerOptions()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };
        _ = RefreshTokenAsync(option.Key, option.Secret).Result;
    }

    public async Task<bool> RefreshTokenAsync(string key, string secret)
    {
        // 请求类型
        var data = new LoginDto
        {
            Password = secret,
            UserName = key,
        };

        var res = await Http.PostAsJsonAsync("/api/admin/Auth", data, JsonSerializerOptions);
        if (res.IsSuccessStatusCode)
        {
            var resData = await res.Content.ReadFromJsonAsync<AuthResult>();
            AccessToken = resData?.Token;
            Http.DefaultRequestHeaders.TryAddWithoutValidation("Authorize", resData?.Token);
            return true;
        }
        else
        {
            ErrorMsg = await res.Content.ReadFromJsonAsync<ErrorResult>();
        }
        return false;
    }


    public void SetBaseUrl(string url)
    {
        this.BaseUrl = url;
    }

}

public class ErrorResult
{
    public string? Title { get; set; }
    public string? Detail { get; set; }
    public int Status { get; set; } = 500;
    public string? TraceId { get; set; }
}

public class AuthOption
{
    public required string Key { get; set; }
    public required string Secret { get; set; }
}
