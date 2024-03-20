using Http.API;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
// 1 添加默认组件
builder.AddDefaultComponents();
// 2 注册和配置Web服务依赖
builder.AddDefaultWebServices();

WebApplication app = builder.Build();
app.UseDefaultWebServices();

using (app)
{
    app.Start();
    // 初始化工作
    await using (AsyncServiceScope scope = app.Services.CreateAsyncScope())
    {
        IServiceProvider provider = scope.ServiceProvider;
        await InitDataTask.InitDataAsync(provider);
    }
    app.WaitForShutdown();
}

public partial class Program { }