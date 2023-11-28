using Dusi.Manage.Client;

namespace DusiApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("FontAwesome6FreeBrands.otf", "FontAwesomeBrands");
                fonts.AddFont("FontAwesome6FreeRegular.otf", "FontAwesomeRegular");
                fonts.AddFont("FontAwesome6FreeSolid.otf", "FontAwesomeSolid");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // 登录
        builder.Services.AddSingleton<SignInViewModel>();
        builder.Services.AddSingleton<SignInPage>();

        // 资讯
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<MainPage>();

        // 设置
        builder.Services.AddSingleton<SettingViewModel>();
        builder.Services.AddSingleton<SettingPage>();

        // 审核列表
        builder.Services.AddSingleton<NewsListViewModel>();
        builder.Services.AddSingleton<NewsListPage>();

        // 详情
        builder.Services.AddSingleton<DetailViewModel>();
        builder.Services.AddSingleton<DetailPage>();


        return builder.Build();
    }
}
