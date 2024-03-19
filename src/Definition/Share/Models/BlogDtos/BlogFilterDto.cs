using Entity.CMS;
namespace Share.Models.BlogDtos;
/// <summary>
/// 博客查询筛选
/// </summary>
/// <inheritdoc cref="Entity.CMS.Blog"/>
public class BlogFilterDto : FilterBase
{
    /// <summary>
    /// 语言类型
    /// </summary>
    public LanguageType? LanguageType { get; set; }
    /// <summary>
    /// 标题
    /// </summary>
    [MaxLength(100)]
    public string? Title { get; set; }
    /// <summary>
    /// 作者
    /// </summary>
    [MaxLength(200)]
    public string? Authors { get; set; }
    /// <summary>
    /// 是否公开
    /// </summary>
    public bool? IsPublic { get; set; }
    /// <summary>
    /// 所属目录
    /// </summary>
    public Guid? CatalogId { get; set; }
    /// <summary>
    /// 标签
    /// </summary>
    public string? Tag { get; set; }
    /// <summary>
    /// 日期
    /// </summary>
    public DateOnly? Date { get; set; }

    /// <summary>
    /// 分类
    /// </summary>
    public BlogType? BlogType { get; set; }
    public Guid? UserId { get; set; }
}
