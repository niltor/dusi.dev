using Microsoft.EntityFrameworkCore.Query;

namespace EntityFramework.DBProvider;

public class ContextBase(DbContextOptions options) : DbContext(options)
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
    public DbSet<OpenSourceProduct> OpenSourceProducts { get; set; }

    #endregion

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        List<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> entries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added).ToList();
        foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry? entityEntry in entries)
        {
            Microsoft.EntityFrameworkCore.Metadata.IProperty? property = entityEntry.Metadata.FindProperty("CreatedTime");
            if (property != null && property.ClrType == typeof(DateTimeOffset))
            {
                entityEntry.Property("CreatedTime").CurrentValue = DateTimeOffset.UtcNow;
            }
        }
        entries = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified).ToList();
        foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry? entityEntry in entries)
        {
            Microsoft.EntityFrameworkCore.Metadata.IProperty? property = entityEntry.Metadata.FindProperty("UpdatedTime");
            if (property != null && property.ClrType == typeof(DateTimeOffset))
            {
                entityEntry.Property("UpdatedTime").CurrentValue = DateTimeOffset.UtcNow;

            }
        }
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
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
        OnModelExtendCreating(builder);
    }

    private void OnModelExtendCreating(ModelBuilder modelBuilder)
    {
        IEnumerable<Microsoft.EntityFrameworkCore.Metadata.IMutableEntityType> entityTypes = modelBuilder.Model.GetEntityTypes();
        foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableEntityType entityType in entityTypes)
        {
            if (typeof(IEntityBase).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.Name)
                    .HasKey("Id");
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(ConvertFilterExpression<IEntityBase>(e => !e.IsDeleted, entityType.ClrType));
            }
        }
    }

    private static LambdaExpression ConvertFilterExpression<TInterface>(Expression<Func<TInterface, bool>> filterExpression, Type entityType)
    {
        ParameterExpression newParam = Expression.Parameter(entityType);
        Expression newBody = ReplacingExpressionVisitor.Replace(filterExpression.Parameters.Single(), newParam, filterExpression.Body);

        return Expression.Lambda(newBody, newParam);
    }
}
