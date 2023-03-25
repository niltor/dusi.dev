namespace DusiApp.Views;

public partial class SignInPage : ContentPage
{
    public SignInPage(SignInViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (AppStatusService.IsLogin())
        {
            // …Ë÷√«Î«Ûtoken
            var token = Preferences.Default.Get(Const.AccessToken, string.Empty);

            ApiService.AdminClient.SetToken(token);
            Application.Current.MainPage = new AppShell();
        }
    }
}