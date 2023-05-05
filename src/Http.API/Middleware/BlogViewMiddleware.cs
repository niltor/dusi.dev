namespace Http.API.Middleware;

/// <summary>
/// 博客访问中间件，记录访问量
/// </summary>
public class BlogViewMiddleware
{
    private readonly RequestDelegate _next;

    public BlogViewMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IBlogManager blogManager)
    {
        // get route params
        var routeValues = context.GetRouteData().Values;
        var requestPath = context.Request.Path.Value;

        var id = requestPath?.Split('/')
            .LastOrDefault()?
            .Split('.')
            .FirstOrDefault();

        if (id != null)
        {
            _ = blogManager.UpdateViewCountAsync(new Guid(id.ToString()));
        }
        // 在此处添加自定义逻辑
        await _next(context);
    }
}


public static class BlogViewMiddlewareExtensions
{
    public static IApplicationBuilder UseBlogViewMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<BlogViewMiddleware>();
    }
}