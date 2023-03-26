namespace DusiApp.ViewModels;
public partial class SettingViewModel : BaseViewModel
{
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
