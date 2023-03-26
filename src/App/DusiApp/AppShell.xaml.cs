using Android.Views.Animations;

namespace DusiApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));

    }

    protected override void OnAppearing()
    {
        // 未登录，跳转回登录页
        if (!AppStatusService.IsLogin())
        {
            Application.Current.MainPage = new SignInPage(new SignInViewModel());
        
        }
        base.OnAppearing();
    }
}
