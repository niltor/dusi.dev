using Application.Const;
using Application.Services;

using Dapr.Client;

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

    private async void DoWork(object? state)
    {
        try
        {
            _logger.LogInformation("⚒️ UpdateViewCountTask...");
            var blogIds = await DaprFacade.GetStateAsync<HashSet<Guid>?>(AppConst.BlogViewCacheKey);
            // 查询要更新blog id
            if (blogIds != null && blogIds.Any())
            {
                var keys = blogIds.Select(b => AppConst.PrefixBlogView + b.ToString()).ToList();
                var ids = await DaprFacade.Dapr.GetBulkStateAsync(AppConst.DefaultStateName, keys, 2);

                using var scope = Services.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<CommandDbContext>();

                // 入库更新
                var successIds = new List<SaveStateItem<int>>();
                foreach (var item in ids)
                {
                    if (int.TryParse(item.Value, out int count))
                    {
                        var resCount = await context.Blogs
                            .Where(b => b.Id.ToString() == item.Key.Replace(AppConst.PrefixBlogView, ""))
                            .ExecuteUpdateAsync(s =>
                                s.SetProperty(b => b.ViewCount, b => b.ViewCount + count));

                        if (resCount > 0)
                        {
                            successIds.Add(new SaveStateItem<int>(item.Key, 0, ""));
                        }
                        _logger.LogInformation("item:{id} update {count}", item.Key, count);
                    }
                }
                // 入库后重置为0
                if (successIds != null && successIds.Any())
                {
                    await DaprFacade.Dapr.SaveBulkStateAsync(AppConst.DefaultStateName, successIds, default);
                }

            }
        }
        catch (Exception ex)
        {
            _logger.LogError("更新浏览数量出错 {message} {stack}", ex.Message + ex.InnerException, ex.StackTrace);
        }
    }
    public override void Dispose() => _timer?.Dispose();
}
