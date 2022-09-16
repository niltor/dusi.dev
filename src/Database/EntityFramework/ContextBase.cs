using Core.Entities;
using Core.Entities.EntityDesign;
using Core.Models;

namespace EntityFramework;

public class ContextBase : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<EntityLibrary> EntityLibraries { get; set; }
    public DbSet<EntityModel> EntityModels { get; set; }
    public DbSet<EntityMember> EntityMembers { get; set; }
    public DbSet<EntityMemberConstraint> EntityMemberConstraints { get; set; }


    public ContextBase(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<EntityBase>().UseTpcMappingStrategy();
        builder.Entity<EntityLibrary>(e =>
        {
            e.HasIndex(a => a.Name);
            e.HasIndex(a => a.IsPublic);
        });
        builder.Entity<EntityModel>(e =>
        {
            e.HasIndex(a => a.Name);
            e.HasIndex(a => a.AccessModifier);
            e.HasIndex(a => a.CodeLanguage);
        });
        builder.Entity<EntityMember>(e =>
        {
            e.HasIndex(a => a.Name);
            e.HasIndex(a => a.AccessModifier);
            e.HasOne(a => a.ObjectType)
                .WithOne()
                .HasForeignKey<EntityMember>(a => a.ObjectTypeId);
            e.HasOne(a => a.Constraint)
                .WithOne(c => c.EntityMember)
                .HasForeignKey<EntityMemberConstraint>(c => c.EntityMemberId);
        });



        builder.Entity<User>(e =>
        {
            e.HasIndex(a => a.Email);
            e.HasIndex(a => a.PhoneNumber);
            e.HasIndex(a => a.UserName);
            e.HasIndex(a => a.IsDeleted);
            e.HasIndex(a => a.CreatedTime);
        });

        builder.Entity<Role>(e =>
        {
            e.HasIndex(m => m.Name);
        });

        base.OnModelCreating(builder);
    }
}
