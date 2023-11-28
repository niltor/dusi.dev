using Ater.Web.Core.Models;

namespace EntityFramework;
public class CommandDbContext : ContextBase
{
    public CommandDbContext(DbContextOptions<CommandDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<IEntityBase>().HasQueryFilter(e => !e.IsDeleted);
        base.OnModelCreating(builder);
    }

}
