namespace Application.CommandStore;
public class ThirdVideoCommandStore : CommandSet<ThirdVideo>
{
    public ThirdVideoCommandStore(CommandDbContext context, ILogger<ThirdVideoCommandStore> logger) : base(context, logger)
    {
    }

}
