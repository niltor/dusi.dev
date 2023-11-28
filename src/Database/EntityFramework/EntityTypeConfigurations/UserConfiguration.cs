using Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace EntityFramework.EntityTypeConfigurations;
internal class UserConfiguration : EntityBaseConfiguration<SystemUser>
{
    public override void Configure(EntityTypeBuilder<SystemUser> builder)
    {
        base.Configure(builder);
    }
}
