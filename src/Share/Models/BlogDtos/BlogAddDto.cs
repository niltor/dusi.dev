using Core.Entities.CMS;
using Google.Protobuf;

namespace Share.Models.BlogDtos;
/// <summary>
/// 博客添加时请求结构
/// </summary>
/// <inheritdoc cref="Core.Entities.CMS.Blog"/>
public class BlogAddDto
{
    /// <summary>
    /// 语言类型
    /// </summary>
    public LanguageType LanguageType { get; set; } = LanguageType.CN;
    /// <summary>
    /// 标题
    /// </summary>
    [MaxLength(100)]
    public required string Title { get; set; }
    /// <summary>
    /// 描述
    /// </summary>
    [MaxLength(300)]
    public string? Description { get; set; }
    /// <summary>
    /// 内容
    /// </summary>
    [MaxLength(10000)]
    public required string Content { get; set; }
    /// <summary>
    /// 是否公开
    /// </summary>
    public bool IsPublic { get; set; } = true;
    /// <summary>
    /// 是否原创
    /// </summary>
    public bool IsOriginal { get; set; }
    /// <summary>
    /// 所属目录
    /// </summary>
    public required Guid CatalogId { get; set; }
    /// <summary>
    /// 标签
    /// </summary>
    public List<string>? Tags { get; set; }

}
