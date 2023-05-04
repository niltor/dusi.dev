using Core.Entities.CMS;
namespace Share.Models.BlogDtos;
/// <summary>
/// 博客列表元素
/// </summary>
/// <inheritdoc cref="Core.Entities.CMS.Blog"/>
public class BlogItemDto
{
    /// <summary>
    /// 语言类型
    /// </summary>
    public LanguageType LanguageType { get; set; } = LanguageType.CN;
    /// <summary>
    /// 标题
    /// </summary>
    [MaxLength(100)]
    public string Title { get; set; } = default!;
    /// <summary>
    /// 描述
    /// </summary>
    [MaxLength(300)]
    public string? Description { get; set; }
    /// <summary>
    /// 作者
    /// </summary>
    [MaxLength(200)]
    public string Authors { get; set; } = default!;
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedTime { get; set; } = DateTimeOffset.UtcNow;
    /// <summary>
    /// 所属目录
    /// </summary>
    public Catalog Catalog { get; set; } = default!;
    public List<Tags>? Tags { get; set; }
    public bool IsPublic { get; set; }
    public BlogType BlogType { get; set; }
}
