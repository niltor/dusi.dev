using CommunityToolkit.Maui.Alerts;
using Dusi.Manage.Client;

namespace DusiApp.Services;
public static class ApiService
{
    public readonly static AdminClient AdminClient = new("http://10.0.2.2:5002/", new InterceptHttpHandler());
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
        var res = await base.SendAsync(request, cancellationToken);

        try
        {
            // 统一处理401的情况
            if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Preferences.Default.Set(Const.AccessToken, string.Empty);
                Preferences.Default.Set(Const.UserName, string.Empty);
                Application.Current.MainPage = new SignInPage(new SignInViewModel());
            }

            return res;
        }
        catch (TaskCanceledException)
        {
            await Toast.Make("请求超时，请稍候重试", CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
            return res;
        }
    }
}