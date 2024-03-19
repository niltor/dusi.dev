// 本文件由 ater.dry工具自动生成.
namespace Application;

public static partial class ManagerServiceCollectionExtensions
{
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
