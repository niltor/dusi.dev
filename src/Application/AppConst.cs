using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application;
public static class AppConst
{
    public const string DefaultStateName = "statestore";
    public const string DefaultPubSubName = "pubsub";

    /// <summary>
    /// 浏览量前缀
    /// </summary>
    public const string PrefixBlogView = "blogView";
    /// <summary>
    /// 博客id缓存 key
    /// </summary>
    public const string BlogViewCacheKey = "blogViewCacheKey";
}
