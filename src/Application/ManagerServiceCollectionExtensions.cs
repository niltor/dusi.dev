namespace Application;

public static class ManagerServiceCollectionExtensions{
    public static void AddDataStore(this IServiceCollection services)
    {
        services.AddScoped(typeof(DataStoreContext));
        services.AddScoped(typeof(BlogQueryStore));
        services.AddScoped(typeof(CatalogQueryStore));
        services.AddScoped(typeof(EntityLibraryQueryStore));
        services.AddScoped(typeof(EntityMemberConstraintQueryStore));
        services.AddScoped(typeof(EntityMemberQueryStore));
        services.AddScoped(typeof(EntityModelQueryStore));
        services.AddScoped(typeof(OpenSourceProductQueryStore));
        services.AddScoped(typeof(SystemLogsQueryStore));
        services.AddScoped(typeof(SystemRoleQueryStore));
        services.AddScoped(typeof(SystemUserQueryStore));
        services.AddScoped(typeof(TagsQueryStore));
        services.AddScoped(typeof(ThirdNewsQueryStore));
        services.AddScoped(typeof(ThirdVideoQueryStore));
        services.AddScoped(typeof(UserQueryStore));
        services.AddScoped(typeof(BlogCommandStore));
        services.AddScoped(typeof(CatalogCommandStore));
        services.AddScoped(typeof(EntityLibraryCommandStore));
        services.AddScoped(typeof(EntityMemberCommandStore));
        services.AddScoped(typeof(EntityMemberConstraintCommandStore));
        services.AddScoped(typeof(EntityModelCommandStore));
        services.AddScoped(typeof(OpenSourceProductCommandStore));
        services.AddScoped(typeof(SystemLogsCommandStore));
        services.AddScoped(typeof(SystemRoleCommandStore));
        services.AddScoped(typeof(SystemUserCommandStore));
        services.AddScoped(typeof(TagsCommandStore));
        services.AddScoped(typeof(ThirdNewsCommandStore));
        services.AddScoped(typeof(ThirdVideoCommandStore));
        services.AddScoped(typeof(UserCommandStore));

    }

    public static void AddManager(this IServiceCollection services)
    {
        services.AddTransient<IUserContext, UserContext>();
        services.AddScoped<IBlogManager, BlogManager>();
        services.AddScoped<ICatalogManager, CatalogManager>();
        services.AddScoped<IEntityLibraryManager, EntityLibraryManager>();
        services.AddScoped<IEntityMemberConstraintManager, EntityMemberConstraintManager>();
        services.AddScoped<IEntityMemberManager, EntityMemberManager>();
        services.AddScoped<IEntityModelManager, EntityModelManager>();
        services.AddScoped<IOpenSourceProductManager, OpenSourceProductManager>();
        services.AddScoped<ISystemLogsManager, SystemLogsManager>();
        services.AddScoped<ISystemRoleManager, SystemRoleManager>();
        services.AddScoped<ISystemUserManager, SystemUserManager>();
        services.AddScoped<ITagsManager, TagsManager>();
        services.AddScoped<IThirdNewsManager, ThirdNewsManager>();
        services.AddScoped<IThirdVideoManager, ThirdVideoManager>();
        services.AddScoped<IUserManager, UserManager>();

    }
}
