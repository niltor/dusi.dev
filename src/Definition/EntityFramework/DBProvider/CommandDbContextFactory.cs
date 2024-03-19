using Microsoft.Extensions.Caching.Distributed;

namespace EntityFramework.DBProvider;
public class CommandDbContextFactory(ITenantProvider tenantProvider, IDistributedCache cache) : IDbContextFactory<CommandDbContext>
{
    private readonly ITenantProvider _tenantProvider = tenantProvider;
    private readonly IDistributedCache _cache = cache;

    public CommandDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<CommandDbContext> optionsBuilder = new();
        Guid tenantId = _tenantProvider.TenantId;

        // 从缓存或配置中查询连接字符串
        string? connectionStrings = _cache.GetString($"{tenantId}_CommandConnectionString");

        optionsBuilder.UseNpgsql(connectionStrings);
        return new CommandDbContext(optionsBuilder.Options);
    }
}
