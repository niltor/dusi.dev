namespace EntityFramework.QueryStore;
public class EntityLibraryQueryStore : QuerySet<EntityLibrary>
{
    public EntityLibraryQueryStore(QueryDbContext context, ILogger<EntityLibraryQueryStore> logger) : base(context, logger)
    {
    }
}


