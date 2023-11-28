namespace EntityFramework.CommandStore;
public class EntityMemberCommandStore : CommandSet<EntityMember>
{
    public EntityMemberCommandStore(CommandDbContext context, ILogger<EntityMemberCommandStore> logger) : base(context, logger)
    {
    }

}
