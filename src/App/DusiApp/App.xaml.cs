namespace DusiApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new SignInPage(new SignInViewModel());
	}
}
