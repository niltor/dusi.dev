using Application.IManager;
using Application.Implement;
using Application.Manager;
using Application.Services;
using Ater.MetaWeBlog.Options;
using Share.Options;
using TaskService.Implement.BlogPublisher;
using TaskService.Implement.NewsCollector;
using TaskService.Implement.NewsCollector.RssFeeds;
using TaskService.Implement.PostBlog;
using TaskService.Tasks;

var builder = WebApplication.CreateBuilder();
var services = builder.Services;
var configuration = builder.Configuration;

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
// 配置
services.Configure<AzureOption>(configuration.GetSection("Azure"));
services.Configure<MetaWeblogOption>(configuration.GetSection("Options:Cnblog"));

// 依赖注入
services.AddDataStore();
services.AddManager();
services.AddSingleton<StorageService>();
services.AddSingleton<MicrosoftFeed>();
services.AddSingleton<OsChinaFeed>();
services.AddSingleton<InfoQFeed>();
services.AddSingleton<RssHelper>();
services.AddScoped<NewsCollector>();
services.AddSingleton<IBlogPublisher, CnBlogPublisher>();
// 后台服务
services.AddHostedService<NewsCollectTask>();
services.AddHostedService<UpdateViewCountTask>();
services.AddHostedService<GetBiliBiliVideosTask>();
services.AddHealthChecks();
// controller with  dapr support
services.AddControllers().AddDapr();

var app = builder.Build();

app.UseHealthChecks("/health");

app.UseCloudEvents();
app.MapControllers();
app.MapSubscribeHandler();

app.Run();
