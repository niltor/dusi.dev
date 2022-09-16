namespace Application.CommandStore;
public class EntityLibraryCommandStore : CommandSet<EntityLibrary>
{
    public EntityLibraryCommandStore(CommandDbContext context, ILogger<EntityLibraryCommandStore> logger) : base(context, logger)
    {
    }

}
