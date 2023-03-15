using TaskService.Implement.NewsCollector;
namespace TaskService.Tasks;

/// <summary>
/// 新闻定时采集任务
/// </summary>
public class NewsCollectTask : BackgroundService
{
    private readonly ILogger<NewsCollectTask> _logger;
    private Timer? _timer;
    private readonly IServiceProvider Services;

    public NewsCollectTask(ILogger<NewsCollectTask> logger, IServiceProvider services)
    {
        _logger = logger;
        Services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }


    public override Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("News collection service start.");
        _timer = new Timer(DoWork, stoppingToken, TimeSpan.Zero, TimeSpan.FromHours(4));
        return Task.CompletedTask;
    }

    private async void DoWork(object? state)
    {
        using var scope = Services.CreateScope();
        var newsService = scope.ServiceProvider.GetRequiredService<NewsCollector>();
        await newsService.Start();
    }

    public override Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");
        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public override void Dispose() => _timer?.Dispose();
}
