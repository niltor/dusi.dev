namespace EntityFramework.QueryStore;
public class EntityMemberQueryStore : QuerySet<EntityMember>
{
    public EntityMemberQueryStore(QueryDbContext context, ILogger<EntityMemberQueryStore> logger) : base(context, logger)
    {
    }
}


