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
            Application.Current.MainPage = new AppShell();
        }
    }
}