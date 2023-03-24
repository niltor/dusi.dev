using System.Net.Http.Json;
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

    public SignInViewModel()
    {
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
            Preferences.Default.Set("AccessToken", res.Token);
            Preferences.Default.Set("Username", res.Username);
            // 跳转页面
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            // TODO:提示错误内容
            await Application.Current.MainPage.DisplayAlert("错误", auth.ErrorMsg.Detail, "确定");
        }
    }

}
