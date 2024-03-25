using Microsoft.Extensions.Caching.Memory;

namespace Http.API.Middleware;

/// <summary>
/// 博客访问中间件，记录访问量
/// </summary>
public class BlogViewMiddleware(RequestDelegate next, IMemoryCache cache, IServiceScopeFactory serviceScopeFactory)
{
    private readonly RequestDelegate _next = next;
    private readonly IMemoryCache _cache = cache;
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

    public async Task Invoke(HttpContext context, ILogger<BlogViewMiddleware> _logger)
    {
        // get route params
        RouteValueDictionary routeValues = context.GetRouteData().Values;
        string? requestPath = context.Request.Path.Value;

        if (requestPath != null
            && requestPath.StartsWith("/blogs")
            && requestPath.EndsWith(".html"))
        {
            string? idstr = requestPath.Split('/')
                .LastOrDefault()?
                .Split('.')
                .FirstOrDefault();

            if (idstr != null)
            {
                MemoryCacheEntryOptions cacheOption = new MemoryCacheEntryOptions()
                   .SetPriority(CacheItemPriority.NeverRemove)
                   .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                cacheOption.PostEvictionCallbacks.Add(new PostEvictionCallbackRegistration()
                {
                    EvictionCallback = (key, value, reason, state) =>
                    {
                        using IServiceScope scope = _serviceScopeFactory.CreateScope();
                        CommandDbContext _context = scope.ServiceProvider.GetRequiredService<CommandDbContext>();
                        _logger.LogInformation("cache expired");
                        if (reason == EvictionReason.Expired)
                        {
                            // update  blog view number 
                            _context.Blogs.Where(b => b.Id == new Guid(idstr))
                                .ExecuteUpdate(s => s.SetProperty(p => p.ViewCount, p => p.ViewCount + (int)value!));
                        }
                    }
                });

                // get cache value by idstr
                if (_cache.TryGetValue(idstr, out int number))
                {
                    number++;
                    _cache.Set(idstr, number, cacheOption);
                }
                else
                {
                    _cache.Set(idstr, 1, cacheOption);
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