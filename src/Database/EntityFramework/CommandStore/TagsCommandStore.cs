namespace EntityFramework.CommandStore;
public class TagsCommandStore : CommandSet<Tags>
{
    public TagsCommandStore(CommandDbContext context, ILogger<TagsCommandStore> logger) : base(context, logger)
    {
    }

}
