// 本文件由 ater.dry工具自动生成.
namespace Application;

public static partial class ManagerServiceCollectionExtensions
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
        services.AddScoped(typeof(BlogManager));
        services.AddScoped(typeof(CatalogManager));
        services.AddScoped(typeof(EntityLibraryManager));
        services.AddScoped(typeof(EntityMemberConstraintManager));
        services.AddScoped(typeof(EntityMemberManager));
        services.AddScoped(typeof(EntityModelManager));
        services.AddScoped(typeof(OpenSourceProductManager));
        services.AddScoped(typeof(SystemLogsManager));
        services.AddScoped(typeof(SystemRoleManager));
        services.AddScoped(typeof(SystemUserManager));
        services.AddScoped(typeof(TagsManager));
        services.AddScoped(typeof(ThirdNewsManager));
        services.AddScoped(typeof(ThirdVideoManager));
        services.AddScoped(typeof(UserManager));

    }
}
