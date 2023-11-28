namespace EntityFramework.QueryStore;
public class ThirdVideoQueryStore : QuerySet<ThirdVideo>
{
    public ThirdVideoQueryStore(QueryDbContext context, ILogger<ThirdVideoQueryStore> logger) : base(context, logger)
    {
    }
}


