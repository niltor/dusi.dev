using TaskService;
using TaskService.Tasks;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<NewsCollectTask>();
    })
    .Build();

host.Run();