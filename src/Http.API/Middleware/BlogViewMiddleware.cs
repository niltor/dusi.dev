using Application;

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
            if (idstr != null)
            {
                var id = new Guid(idstr);
                try
                {
                    // 统计浏览量,使用缓存
                    // 缓存的blog id
                    HashSet<Guid>? blogIds = await DaprFacade.GetStateAsync<HashSet<Guid>?>(AppConst.BlogViewCacheKey);
                    // 初始添加
                    int ttl = 7 * 24 * 60 * 60;
                    if (blogIds == null)
                    {
                        HashSet<Guid> set = new()
                        {
                            id
                        };
                        await DaprFacade.SaveStateAsync(AppConst.BlogViewCacheKey, set, ttl);
                    }
                    else
                    {
                        // 新数据添加后更新到缓存
                        if (blogIds.Add(id))
                        {
                            await DaprFacade.SaveStateAsync(AppConst.BlogViewCacheKey, blogIds, ttl);
                        }
                    }
                    _logger.LogInformation("new blog view id:{id}", id);
                    // 数量存缓存
                    int? count = await DaprFacade.GetStateAsync<int?>(AppConst.PrefixBlogView + id.ToString());
                    if (count == null)
                    {
                        // 10分钟
                        await DaprFacade.SaveStateAsync(AppConst.PrefixBlogView + id.ToString(), 1, 10 * 60);
                    }
                    else
                    {
                        count++;
                        await DaprFacade.SaveStateAsync(AppConst.PrefixBlogView + id.ToString(), count, 10 * 60);
                    }
                    _logger.LogInformation("view count:{count}", count);
                }
                catch (Exception ex)
                {
                    _logger.LogError("update error:{message}", ex.Message + ex.StackTrace);
                }

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