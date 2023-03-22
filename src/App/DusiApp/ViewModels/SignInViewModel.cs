using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

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

        await Console.Out.WriteLineAsync("123");
        // TODO:登录获取token
        await Application.Current.MainPage.DisplayPromptAsync("内容", Username + Password);
    }

}
