using Core.Entities;
using Core.Entities.CMS;
using Core.Entities.EntityDesign;
using Core.Models;

namespace EntityFramework;

public class ContextBase : DbContext
{
    public DbSet<SystemUser> SystemUsers { get; set; }
    public DbSet<SystemRole> SystemRoles { get; set; }
    public DbSet<WebConfig> WebConfigs { get; set; }
    public DbSet<EntityLibrary> EntityLibraries { get; set; }
    public DbSet<EntityModel> EntityModels { get; set; }
    public DbSet<EntityMember> EntityMembers { get; set; }
    public DbSet<EntityMemberConstraint> EntityMemberConstraints { get; set; }
    public DbSet<User> Users { get; set; }

    #region CMS entities
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Tags> Tags { get; set; }
    public DbSet<Catalog> Catalogs { get; set; }
    public DbSet<ThirdNews> ThirdNews { get; set; }
    public DbSet<ThirdVideo> ThirdVideos { get; set; }
    #endregion

    public ContextBase(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<EntityBase>().UseTpcMappingStrategy();

        builder.Entity<Blog>(e =>
        {
            e.HasIndex(b => b.Title);
            e.HasIndex(b => b.LanguageType);
            e.HasIndex(b => b.Authors);
            e.HasIndex(b => b.IsOriginal);
            e.HasIndex(b => b.IsPublic);
            e.HasIndex(b => b.CreatedTime);

        });
        builder.Entity<User>(e =>
        {
            e.HasIndex(m => m.UserName).IsUnique();
        });

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

        builder.Entity<SystemUser>(e =>
        {
            e.HasIndex(a => a.Email);
            e.HasIndex(a => a.PhoneNumber);
            e.HasIndex(a => a.UserName);
            e.HasIndex(a => a.IsDeleted);
            e.HasIndex(a => a.CreatedTime);
        });

        builder.Entity<SystemRole>(e =>
        {
            e.HasIndex(m => m.Name);
        });

        base.OnModelCreating(builder);
    }
}
