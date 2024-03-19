using Microsoft.Extensions.Caching.Distributed;

namespace EntityFramework.DBProvider;
public class QueryDbContextFactory(ITenantProvider tenantProvider, IDistributedCache cache) : IDbContextFactory<QueryDbContext>
{
    private readonly ITenantProvider _tenantProvider = tenantProvider;
    private readonly IDistributedCache _cache = cache;

    public QueryDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<QueryDbContext> optionsBuilder = new();
        Guid tenantId = _tenantProvider.TenantId;

        // 从缓存或配置中查询连接字符串
        string? connectionStrings = _cache.GetString($"{tenantId}_QueryConnectionString");

        optionsBuilder.UseNpgsql(connectionStrings);
        return new QueryDbContext(optionsBuilder.Options);
    }
}
