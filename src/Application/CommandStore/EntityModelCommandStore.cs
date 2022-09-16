namespace Application.CommandStore;
public class EntityModelCommandStore : CommandSet<EntityModel>
{
    public EntityModelCommandStore(CommandDbContext context, ILogger<EntityModelCommandStore> logger) : base(context, logger)
    {
    }

}
