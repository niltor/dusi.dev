using Core.Entities.CMS;
namespace Share.Models.ThirdNewsDtos;

/// <inheritdoc cref="Core.Entities.CMS.ThirdNews"/>
public class ThirdNewsItemDto
{
    [MaxLength(100)]
    public string? AuthorName { get; set; }
    [MaxLength(300)]
    public string? AuthorAvatar { get; set; }
    [MaxLength(200)]
    public string Title { get; set; } = default!;
    [MaxLength(300)]
    public string? Url { get; set; }
    [MaxLength(300)]
    public string? ThumbnailUrl { get; set; }
    [MaxLength(50)]
    public string? Provider { get; set; }
    public DateTimeOffset? DatePublished { get; set; }
    [MaxLength(50)]
    public string? Category { get; set; }
    /// <summary>
    /// ธลาช
    /// </summary>
    [MaxLength(5000)]
    public string? Description { get; set; }

    [MaxLength(50)]
    public string? IdentityId { get; set; }
    public NewsSource Type { get; set; } = NewsSource.News;
    public NewsType NewsType { get; set; } = NewsType.Default;
    public TechType TechType { get; set; } = TechType.Default;
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedTime { get; set; } = DateTimeOffset.UtcNow;

}
