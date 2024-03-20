using Application;
using Application.Implement;
using Application.Services;
using Share.Options;
using TaskService.Implement.NewsCollector;
using TaskService.Implement.NewsCollector.RssFeeds;
using TaskService.Tasks;

Console.OutputEncoding = Encoding.UTF8;

WebApplicationBuilder builder = WebApplication.CreateBuilder();
IServiceCollection services = builder.Services;
ConfigurationManager configuration = builder.Configuration;

string? azAppConfigConnection = builder.Configuration["AppConfig"];
if (!string.IsNullOrEmpty(azAppConfigConnection))
{
    builder.Configuration.AddAzureAppConfiguration(options =>
    {
        options.Connect(azAppConfigConnection)
        .ConfigureRefresh(refresh =>
        {
            refresh.Register("ConfigVersion", refreshAll: true);
        });
    });
}
services.AddAzureAppConfiguration();

string? connectionString = configuration.GetConnectionString("Default");
services.AddDbContextPool<QueryDbContext>(option =>
{
    _ = option.UseNpgsql(connectionString, sql =>
    {
        _ = sql.MigrationsAssembly("Http.API");
        _ = sql.CommandTimeout(10);
    });
});
services.AddDbContextPool<CommandDbContext>(option =>
{
    _ = option.UseNpgsql(connectionString, sql =>
    {
        _ = sql.MigrationsAssembly("Http.API");
        _ = sql.CommandTimeout(10);
    });
});

services.Configure<AzureOption>(configuration.GetSection("Azure"));
services.Configure<MetaWeblogOption>(configuration.GetSection("Options:Cnblog"));

services.AddHttpContextAccessor();
services.AddTransient<IUserContext, UserContext>();
services.AddManager();
services.AddSingleton<StorageService>();
services.AddSingleton<MicrosoftFeed>();
services.AddSingleton<OsChinaFeed>();
services.AddSingleton<InfoQFeed>();
services.AddSingleton<RssHelper>();
services.AddScoped<NewsCollector>();
services.AddHostedService<NewsCollectTask>();
services.AddHostedService<UpdateViewCountTask>();
services.AddHostedService<GetBiliBiliVideosTask>();
services.AddHealthChecks();

WebApplication app = builder.Build();
//app.UseAzureAppConfiguration();

app.UseHealthChecks("/health");

app.Run();
