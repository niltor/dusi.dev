using Application.Implement;
using Application.Services;
using Share.Options;
using TaskService.Implement.BlogPublisher;
using TaskService.Implement.NewsCollector;
using TaskService.Implement.NewsCollector.RssFeeds;
using TaskService.Implement.PostBlog;
using TaskService.Tasks;

Console.OutputEncoding = Encoding.UTF8;

var builder = WebApplication.CreateBuilder();
var services = builder.Services;
var configuration = builder.Configuration;

// ����
var azAppConfigConnection = builder.Configuration["AppConfig"];
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

services.AddHttpContextAccessor();
services.Configure<AzureOption>(configuration.GetSection("Azure"));
services.Configure<MetaWeblogOption>(configuration.GetSection("Options:Cnblog"));

// ����ע��
services.AddDataStore();
services.AddManager();
services.AddSingleton<StorageService>();
services.AddSingleton<MicrosoftFeed>();
services.AddSingleton<OsChinaFeed>();
services.AddSingleton<InfoQFeed>();
services.AddSingleton<RssHelper>();
services.AddScoped<NewsCollector>();
services.AddSingleton<IBlogPublisher, CnBlogPublisher>();
// ��̨����
services.AddHostedService<NewsCollectTask>();
services.AddHostedService<UpdateViewCountTask>();
services.AddHostedService<GetBiliBiliVideosTask>();
services.AddHealthChecks();
// controller with  dapr support
services.AddControllers().AddDapr();

var app = builder.Build();
//app.UseAzureAppConfiguration();

app.UseHealthChecks("/health");

app.UseCloudEvents();
app.MapControllers();
app.MapSubscribeHandler();

app.Run();
