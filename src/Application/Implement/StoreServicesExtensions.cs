namespace Application.Implement;

public static class StoreServicesExtensions
{
    public static void AddDataStore(this IServiceCollection services)
    {
        _ = services.AddScoped(typeof(DataStoreContext));
        _ = services.AddScoped(typeof(BlogQueryStore));
        _ = services.AddScoped(typeof(CatalogQueryStore));
        _ = services.AddScoped(typeof(EntityLibraryQueryStore));
        _ = services.AddScoped(typeof(EntityMemberConstraintQueryStore));
        _ = services.AddScoped(typeof(EntityMemberQueryStore));
        _ = services.AddScoped(typeof(EntityModelQueryStore));
        _ = services.AddScoped(typeof(SystemLogsQueryStore));
        _ = services.AddScoped(typeof(SystemRoleQueryStore));
        _ = services.AddScoped(typeof(SystemUserQueryStore));
        _ = services.AddScoped(typeof(TagsQueryStore));
        _ = services.AddScoped(typeof(ThirdNewsQueryStore));
        _ = services.AddScoped(typeof(ThirdVideoQueryStore));
        _ = services.AddScoped(typeof(UserQueryStore));
        _ = services.AddScoped(typeof(BlogCommandStore));
        _ = services.AddScoped(typeof(CatalogCommandStore));
        _ = services.AddScoped(typeof(EntityLibraryCommandStore));
        _ = services.AddScoped(typeof(EntityMemberCommandStore));
        _ = services.AddScoped(typeof(EntityMemberConstraintCommandStore));
        _ = services.AddScoped(typeof(EntityModelCommandStore));
        _ = services.AddScoped(typeof(SystemLogsCommandStore));
        _ = services.AddScoped(typeof(SystemRoleCommandStore));
        _ = services.AddScoped(typeof(SystemUserCommandStore));
        _ = services.AddScoped(typeof(TagsCommandStore));
        _ = services.AddScoped(typeof(ThirdNewsCommandStore));
        _ = services.AddScoped(typeof(ThirdVideoCommandStore));
        _ = services.AddScoped(typeof(UserCommandStore));

    }

    public static void AddManager(this IServiceCollection services)
    {
        _ = services.AddTransient<IUserContext, UserContext>();
        _ = services.AddScoped<IBlogManager, BlogManager>();
        _ = services.AddScoped<ICatalogManager, CatalogManager>();
        _ = services.AddScoped<IEntityLibraryManager, EntityLibraryManager>();
        _ = services.AddScoped<IEntityMemberConstraintManager, EntityMemberConstraintManager>();
        _ = services.AddScoped<IEntityMemberManager, EntityMemberManager>();
        _ = services.AddScoped<IEntityModelManager, EntityModelManager>();
        _ = services.AddScoped<ISystemLogsManager, SystemLogsManager>();
        _ = services.AddScoped<ISystemRoleManager, SystemRoleManager>();
        _ = services.AddScoped<ISystemUserManager, SystemUserManager>();
        _ = services.AddScoped<ITagsManager, TagsManager>();
        _ = services.AddScoped<IThirdNewsManager, ThirdNewsManager>();
        _ = services.AddScoped<IThirdVideoManager, ThirdVideoManager>();
        _ = services.AddScoped<IUserManager, UserManager>();

    }
}
