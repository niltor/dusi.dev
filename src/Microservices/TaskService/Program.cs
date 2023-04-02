using Application.Services;
using Share.Options;
using TaskService.Implement.NewsCollector;
using TaskService.Implement.NewsCollector.RssFeeds;
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

services.Configure<AzureOption>(configuration.GetSection("Azure"));
services.AddSingleton<StorageService>();

services.AddSingleton<MicrosoftFeed>();
services.AddSingleton<OsChinaFeed>();
services.AddSingleton<InfoWorldFeed>();
services.AddSingleton<RssHelper>();
services.AddScoped<NewsCollector>();
services.AddHostedService<NewsCollectTask>();
services.AddHostedService<UpdateViewCountTask>();
services.AddHostedService<GetBiliBiliVideosTask>();
services.AddHealthChecks();

var app = builder.Build();

app.UseHealthChecks("/health");
using (app)
{
    app.Start();
    app.WaitForShutdown();
}
