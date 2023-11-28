using Entity.CMS;
namespace Share.Models.ThirdVideoDtos;
/// <summary>
/// 三方视频列表元素
/// </summary>
/// <inheritdoc cref="Entity.CMS.ThirdVideo"/>
public class ThirdVideoItemDto
{
    [MaxLength(120)]
    public string Title { get; set; } = default!;
    [MaxLength(500)]
    public string? Description { get; set; }
    [MaxLength(200)]
    public string? ThumbnailUrl { get; set; }
    [MaxLength(200)]
    public string OriginalUrl { get; set; } = default!;
    [MaxLength(50)]
    public string? Source { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedTime { get; set; } = DateTimeOffset.UtcNow;
    /// <summary>
    /// 唯一标识 
    /// </summary>
    [MaxLength(100)]
    public string Identity { get; set; } = default!;
}
