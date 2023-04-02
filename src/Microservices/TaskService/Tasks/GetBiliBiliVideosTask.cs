using TaskService.Implement;
using static System.Formats.Asn1.AsnWriter;
using TaskService.Implement.NewsCollector;
using Application.Services;

namespace TaskService.Tasks;

/// <summary>
/// 获取bilibili视频任务
/// </summary>
public class GetBiliBiliVideosTask : BackgroundService
{
    private readonly ILogger<GetBiliBiliVideosTask> _logger;
    private Timer? _timer;
    private readonly IServiceProvider Services;
    private readonly StorageService storage;

    public GetBiliBiliVideosTask(ILogger<GetBiliBiliVideosTask> logger, IServiceProvider services, StorageService storage)
    {
        _logger = logger;
        Services = services;
        this.storage = storage;
    }

    public override async Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("bilibili video service start.");
        await ExecuteAsync(stoppingToken);
    }

    public override Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("bilibili video service  is stopping.");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _timer = new Timer(DoWork, stoppingToken, TimeSpan.FromSeconds(1), TimeSpan.FromHours(4));
        return Task.CompletedTask;
    }

    private async void DoWork(object? state)
    {
        var videoTask = new GetBilibiliVideo();
        try
        {
            using var scope = Services.CreateScope();
            var dbcontext = scope.ServiceProvider.GetRequiredService<CommandDbContext>();

            var data = await videoTask.GetLatestVideoAsync();
            if (data != null && !string.IsNullOrWhiteSpace(data.Identity))
            {
                // 判断是否重复
                var exist = dbcontext.ThirdVideos.Any(v => v.Identity == data.Identity);
                if (exist) { return; }
                // 处理图片:下载重新上传
                using (var http = new HttpClient())
                {
                    var stream = await http.GetStreamAsync(data.ThumbnailUrl + "@320w_200h_1c_!web-space-index-myvideo.webp");
                    var path = Path.Combine("video", "thumbnail");
                    var position = data.ThumbnailUrl?.LastIndexOf("/");
                    if (position is not null and > (-1))
                    {
                        var filename = data.ThumbnailUrl?[(position.Value + 1)..];

                        if (filename != null)
                        {
                            var newPath = await storage.UploadAsync(stream, Path.Combine(path, filename));
                            // 修改成新地址
                            data.ThumbnailUrl = newPath;
                        }
                    }
                }
                // 存储入库
                dbcontext.ThirdVideos.Add(data);
                await dbcontext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Get Latest video error:{message},{stacktrace}", ex.Message, ex.StackTrace);
        }
    }

    public override void Dispose() => _timer?.Dispose();

}
