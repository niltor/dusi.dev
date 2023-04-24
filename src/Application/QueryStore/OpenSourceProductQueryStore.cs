namespace Application.QueryStore;
public class OpenSourceProductQueryStore : QuerySet<OpenSourceProduct>
{
    public OpenSourceProductQueryStore(QueryDbContext context, ILogger<OpenSourceProductQueryStore> logger) : base(context, logger)
    {
    }
}


