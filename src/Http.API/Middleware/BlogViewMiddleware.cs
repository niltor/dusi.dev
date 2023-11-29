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

    public async Task Invoke(HttpContext context, ILogger<BlogViewMiddleware> _logger)
    {
        // get route params
        var routeValues = context.GetRouteData().Values;
        var requestPath = context.Request.Path.Value;

        if (requestPath != null
            && requestPath.StartsWith("/blogs")
            && requestPath.EndsWith(".html"))
        {
            var idstr = requestPath.Split('/')
                .LastOrDefault()?
                .Split('.')
                .FirstOrDefault();

            _logger.LogInformation("the access blog id:{id}", idstr);
            if (idstr != null)
            {
                var id = new Guid(idstr);
            }
        }
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