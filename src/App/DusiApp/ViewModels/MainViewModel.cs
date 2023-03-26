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

}
