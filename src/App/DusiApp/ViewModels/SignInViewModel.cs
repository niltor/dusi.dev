using System.Net.Http.Json;
using Share.Models.AuthDtos;

namespace DusiApp.ViewModels;

/// <summary>
/// 登录时的视图模型
/// </summary>
public partial class SignInViewModel : BaseViewModel
{
    [ObservableProperty]
    public string username;

    [ObservableProperty]
    public string password;

    public SignInViewModel()
    {

    }

    [RelayCommand]
    private async void SignIn()
    {

        var httpclient = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(5)
        };
        var data = new LoginDto
        {
            UserName = Username,
            Password = Password,
        };
        var res = await httpclient.PostAsJsonAsync("http://10.0.2.2:5002/api/admin/auth", data);

        var resStr = await res.Content.ReadAsStringAsync();

        if (res.IsSuccessStatusCode)
        {
            var resData = await res.Content.ReadFromJsonAsync<AuthResult>();

            // 保存token
            Preferences.Default.Set("AccessToken", resData.Token);
            Preferences.Default.Set("Username", resData.Username);
            // 跳转页面
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            // TODO:提示错误内容
            await Application.Current.MainPage.DisplayAlert("错误", "登录失败", "确定");
        }
    }

}
