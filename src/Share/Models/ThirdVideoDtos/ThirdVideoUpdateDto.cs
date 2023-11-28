using Entity.CMS;
namespace Share.Models.ThirdVideoDtos;
/// <summary>
/// 三方视频更新时请求结构
/// </summary>
/// <inheritdoc cref="Entity.CMS.ThirdVideo"/>
public class ThirdVideoUpdateDto
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
    
}
