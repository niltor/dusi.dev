namespace DusiApp.Views;

public partial class SettingPage : ContentPage
{
    public SettingPage(SettingViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}