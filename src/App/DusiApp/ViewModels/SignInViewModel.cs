using System.Net.Http.Json;
using CommunityToolkit.Maui.Alerts;
using Dusi.Manage.Client;
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

    [ObservableProperty]
    public string token;

    public SignInViewModel()
    {

        Token = Preferences.Default.Get(Const.AccessToken, string.Empty);
    }

    [RelayCommand]
    private async void SignIn()
    {
        var auth = ApiService.AdminClient.Auth;

        var data = new LoginDto
        {
            UserName = Username,
            Password = Password,
        };

        var res = await auth.LoginAsync(data);
        if (res != null)
        {
            ApiService.AdminClient.SetToken(res.Token);
            // 保存token
            Preferences.Default.Set(Const.AccessToken, res.Token);
            Preferences.Default.Set(Const.UserName, res.Username);

            await Toast.Make("登录成功").Show();
            // 跳转页面
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            await Toast.Make("用户名或密码错误").Show();
        }
    }

}
