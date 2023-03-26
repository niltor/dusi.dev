using CommunityToolkit.Maui.Alerts;
using Dusi.Manage.Client;

namespace DusiApp.Services;
public static class ApiService
{
    public readonly static AdminClient AdminClient = new("https://dusi.dev/", new InterceptHttpHandler());
}

/// <summary>
/// httpclient 拦截
/// </summary>
public class InterceptHttpHandler : HttpClientHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        HttpResponseMessage res = null;
        try
        {
            res = await base.SendAsync(request, cancellationToken);

            // 统一处理401的情况
            if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Preferences.Default.Set(Const.AccessToken, string.Empty);
                Preferences.Default.Set(Const.UserName, string.Empty);
                Application.Current.MainPage = new SignInPage(new SignInViewModel());
            }
            return res;

        }
        catch (Exception ex)
        {
            await Toast.Make("请求错误，请稍候重试," + ex.Message).Show();
            return res;
        }
    }
}