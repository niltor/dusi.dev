namespace Application.CommandStore;
public class OpenSourceProductCommandStore : CommandSet<OpenSourceProduct>
{
    public OpenSourceProductCommandStore(CommandDbContext context, ILogger<OpenSourceProductCommandStore> logger) : base(context, logger)
    {
    }

}
