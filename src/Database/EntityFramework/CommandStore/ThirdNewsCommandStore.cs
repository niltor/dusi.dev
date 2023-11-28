namespace EntityFramework.CommandStore;
public class ThirdNewsCommandStore : CommandSet<ThirdNews>
{
    public ThirdNewsCommandStore(CommandDbContext context, ILogger<ThirdNewsCommandStore> logger) : base(context, logger)
    {
    }

}
