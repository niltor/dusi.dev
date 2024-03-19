namespace EntityFramework.QueryStore;
public class ThirdNewsQueryStore : QuerySet<ThirdNews>
{
    public ThirdNewsQueryStore(QueryDbContext context, ILogger<ThirdNewsQueryStore> logger) : base(context, logger)
    {
    }
}


