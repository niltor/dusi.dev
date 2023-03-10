namespace Application.Implement;

public static class StoreServicesExtensions
{
    public static void AddDataStore(this IServiceCollection services)
    {
        services.AddScoped(typeof(DataStoreContext));
        services.AddScoped(typeof(BlogQueryStore));
        services.AddScoped(typeof(CatalogQueryStore));
        services.AddScoped(typeof(EntityLibraryQueryStore));
        services.AddScoped(typeof(EntityMemberConstraintQueryStore));
        services.AddScoped(typeof(EntityMemberQueryStore));
        services.AddScoped(typeof(EntityModelQueryStore));
        services.AddScoped(typeof(SystemRoleQueryStore));
        services.AddScoped(typeof(SystemUserQueryStore));
        services.AddScoped(typeof(TagsQueryStore));
        services.AddScoped(typeof(UserQueryStore));
        services.AddScoped(typeof(BlogCommandStore));
        services.AddScoped(typeof(CatalogCommandStore));
        services.AddScoped(typeof(EntityLibraryCommandStore));
        services.AddScoped(typeof(EntityMemberCommandStore));
        services.AddScoped(typeof(EntityMemberConstraintCommandStore));
        services.AddScoped(typeof(EntityModelCommandStore));
        services.AddScoped(typeof(SystemRoleCommandStore));
        services.AddScoped(typeof(SystemUserCommandStore));
        services.AddScoped(typeof(TagsCommandStore));
        services.AddScoped(typeof(UserCommandStore));

    }

    public static void AddManager(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddTransient<IUserContext, UserContext>();
        services.AddScoped<IBlogManager, BlogManager>();
        services.AddScoped<ICatalogManager, CatalogManager>();
        services.AddScoped<IEntityLibraryManager, EntityLibraryManager>();
        services.AddScoped<IEntityMemberConstraintManager, EntityMemberConstraintManager>();
        services.AddScoped<IEntityMemberManager, EntityMemberManager>();
        services.AddScoped<IEntityModelManager, EntityModelManager>();
        services.AddScoped<ISystemRoleManager, SystemRoleManager>();
        services.AddScoped<ISystemUserManager, SystemUserManager>();
        services.AddScoped<ITagsManager, TagsManager>();
        services.AddScoped<IUserManager, UserManager>();

    }
}
