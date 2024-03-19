namespace TaskService.Tasks;

/// <summary>
/// 浏览量更新服务
/// </summary>
public class UpdateViewCountTask(ILogger<UpdateViewCountTask> logger, IServiceProvider services) : BackgroundService
{
    private readonly ILogger<UpdateViewCountTask> _logger = logger;
    private Timer? _timer;
    private readonly IServiceProvider Services = services;

    public override async Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("UpdateViewCountTask service start.");
        await ExecuteAsync(stoppingToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _timer = new Timer(DoWork, stoppingToken,
            TimeSpan.FromSeconds(10),
            TimeSpan.FromMinutes(2));
        return Task.CompletedTask;
    }

    public override Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");
        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        try
        {

        }
        catch (Exception ex)
        {
            _logger.LogError("更新浏览数量出错 {message} {stack}", ex.Message + ex.InnerException, ex.StackTrace);
        }
    }
    public override void Dispose() => _timer?.Dispose();
}
