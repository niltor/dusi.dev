namespace EntityFramework.QueryStore;
public class EntityMemberConstraintQueryStore : QuerySet<EntityMemberConstraint>
{
    public EntityMemberConstraintQueryStore(QueryDbContext context, ILogger<EntityMemberConstraintQueryStore> logger) : base(context, logger)
    {
    }
}


