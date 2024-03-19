namespace EntityFramework.CommandStore;
public class EntityMemberConstraintCommandStore : CommandSet<EntityMemberConstraint>
{
    public EntityMemberConstraintCommandStore(CommandDbContext context, ILogger<EntityMemberConstraintCommandStore> logger) : base(context, logger)
    {
    }

}
