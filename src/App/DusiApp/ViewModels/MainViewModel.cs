namespace DusiApp.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    [ObservableProperty]
    private string username;

    [ObservableProperty]
    private string token;
    public MainViewModel()
    {
        Token = Preferences.Default.Get(Const.AccessToken, string.Empty);
        Username = Preferences.Default.Get(Const.UserName, string.Empty);
    }

    /// <summary>
    /// 退出
    /// </summary>
    [RelayCommand]
    private void Logout()
    {
        Preferences.Default.Set(Const.AccessToken, string.Empty);
        Preferences.Default.Set(Const.UserName, string.Empty);
        Application.Current.MainPage = new SignInPage(new SignInViewModel());
    }
}
