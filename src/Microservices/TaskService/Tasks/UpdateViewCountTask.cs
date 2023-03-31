using Application.Services;
using Microsoft.Extensions.Logging;
using TaskService.Implement.NewsCollector;

namespace TaskService.Tasks;

/// <summary>
/// 浏览量更新服务
/// </summary>
public class UpdateViewCountTask : BackgroundService
{
    private readonly ILogger<UpdateViewCountTask> _logger;
    private Timer? _timer;
    private readonly IServiceProvider Services;

    public UpdateViewCountTask(ILogger<UpdateViewCountTask> logger, IServiceProvider services)
    {
        _logger = logger;
        Services = services;
    }

    public override Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("News collection service start.");
        return Task.CompletedTask;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _timer = new Timer(DoWork, stoppingToken, TimeSpan.Zero, TimeSpan.FromMinutes(5));
        return Task.CompletedTask;
    }

    public override Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");
        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    private async void DoWork(object? state)
    {
        var blogIds = await DaprFacade.GetStateAsync<HashSet<Guid>?>("blogViewIds");

        // 查询要更新blog id
        if (blogIds != null && blogIds.Any())
        {
            var keys = blogIds.Select(b => "blogView" + b.ToString()).ToList();
            var ids = await DaprFacade.Dapr.GetBulkStateAsync("statestore", keys, 2);

            using var scope = Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CommandDbContext>();

            // 入库更新
            foreach (var item in ids)
            {

                if (int.TryParse(item.Value, out int count))
                {
                    await context.Blogs
                        .Where(b => b.Id.ToString() == item.Key)
                        .ExecuteUpdateAsync(s =>
                            s.SetProperty(b => b.ViewCount, b => b.ViewCount + count));

                }
            }
        }
    }
    public override void Dispose() => _timer?.Dispose();
}
