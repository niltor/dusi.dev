namespace Application.QueryStore;
public class EntityModelQueryStore : QuerySet<EntityModel>
{
    public EntityModelQueryStore(QueryDbContext context, ILogger<EntityModelQueryStore> logger) : base(context, logger)
    {
    }
}


