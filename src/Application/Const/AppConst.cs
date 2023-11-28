namespace Application.Const;
/// <summary>
/// 应用程序常量
/// </summary>
public static class AppConst
{
    public const string DefaultStateName = "statestore";
    public const string DefaultPubSubName = "pubsub";

    /// <summary>
    /// 管理员policy
    /// </summary>
    public const string AdminUser = "AdminUser";

    public const string Admin = "Admin";
    /// <summary>
    /// 普通用户policy
    /// </summary>
    public const string User = "User";

    /// <summary>
    /// 版本
    /// </summary>
    public const string Version = "Version";
    /// <summary>
    /// blog浏览 pub主题
    /// </summary>
    public const string PubBlogView = "PubBlogView";
    public const string PubNewBlog = "PubNewBlog";
    /// <summary>
    /// 浏览量前缀
    /// </summary>
    public const string PrefixBlogView = "blogView";
    /// <summary>
    /// 博客id缓存 key
    /// </summary>
    public const string BlogViewCacheKey = "blogViewCacheKey";
}
