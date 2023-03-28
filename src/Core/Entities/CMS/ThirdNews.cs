using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
namespace Core.Entities.CMS;

/// <summary>
/// 第三方资讯
/// </summary>
[Index(nameof(AuthorName))]
[Index(nameof(Title))]
[Index(nameof(Provider))]
[Index(nameof(Category))]
[Index(nameof(NewsType))]
public class ThirdNews : EntityBase
{
    private string? _description;
    private string? _content;
    private string? _title;
    private string? _url;
    private string? _provider;
    private string? _category;
    [MaxLength(100)]
    public string? AuthorName { get; set; }
    [MaxLength(300)]
    public string? AuthorAvatar { get; set; }

    [MaxLength(200)]
    public required string Title
    {
        get => _title ?? string.Empty;
        set => _title = value != null && value.Length > 200 ? value[..200] : value;
    }
    [MaxLength(5000)]
    public string? Description
    {
        get => _description;
        set => _description = value != null && value.Length > 5000 ? value[..5000] : value;
    }
    [MaxLength(300)]
    public string? Url
    {
        get => _url;
        set => _url = value != null && value.Length > 300 ? value[..300] : value;
    }
    [MaxLength(300)]
    public string? ThumbnailUrl { get; set; }
    [MaxLength(50)]
    public string? Provider
    {
        get => _provider;
        set => _provider = value != null && value.Length > 50 ? value[..50] : value;
    }
    public DateTimeOffset? DatePublished { get; set; }
    [MaxLength(8000)]
    public string? Content
    {
        get => _content;
        set => _content = value != null && value.Length > 8000 ? value[..8000] : value;
    }
    [MaxLength(50)]
    public string? Category
    {
        get => _category;
        set => _category = value != null && value.Length > 50 ? value[..50] : value;
    }
    [MaxLength(50)]
    public string? IdentityId { get; set; }
    public NewsSource Type { get; set; } = NewsSource.News;
    public NewsType NewsType { get; set; } = NewsType.Default;
    public List<NewsTags>? NewsTags { get; set; }
    public TechType TechType { get; set; } = TechType.Default;
    /// <summary>
    /// 第三方资讯状态
    /// </summary>
    public NewsStatus NewsStatus { get; set; }

}

/// <summary>
/// 第三方资讯状态
/// </summary>
public enum NewsStatus
{
    /// <summary>
    /// 默认状态
    /// </summary> 
    [Description("默认")]
    Default,
    /// <summary>
    /// 公开
    /// </summary> 
    [Description("公开")]
    Public,
    /// <summary>
    /// 内部
    /// </summary> 
    [Description("内部")]
    Internal,
}

public enum TechType
{
    [Description("未标记")]
    Default,
    /// <summary>
    /// 常规资讯
    /// </summary>
    [Description("资讯")]
    Normal,
    /// <summary>
    /// 发布或更新
    /// </summary>
    [Description("发布或更新")]
    Publish,
    /// <summary>
    /// 关注内容
    /// </summary>
    [Description("重点关注")]
    Focus
}

public enum NewsSource
{
    News,
    Tweet,
    Weibo,
    Rss
}

public enum NewsType
{
    Default,
    /// <summary>
    /// 风向标
    /// </summary>
    [Description("风向标")]
    News,
    /// <summary>
    /// 开源
    /// </summary>
    [Description("开源和工具")]
    OpenSource,
    /// <summary>
    /// 语言及框架
    /// </summary>
    [Description("语言及框架")]
    LanguageAndFramework,
    /// <summary>
    /// 数据和AI
    /// </summary>
    [Description("AI和数据")]
    DataAndAI,
    /// <summary>
    /// DevOps
    /// </summary>
    [Description("云与DevOps")]
    CloudAndDevOps,
    [Description("其它")]
    Else
}
