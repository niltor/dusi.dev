using Core.Entities.CMS;
namespace Share.Models.ThirdVideoDtos;
/// <summary>
/// 三方视频添加时请求结构
/// </summary>
/// <inheritdoc cref="Core.Entities.CMS.ThirdVideo"/>
public class ThirdVideoAddDto
{
    [MaxLength(120)]
    public required string Title { get; set; }
    [MaxLength(500)]
    public string? Description { get; set; }
    [MaxLength(200)]
    public string? ThumbnailUrl { get; set; }
    [MaxLength(200)]
    public required string OriginalUrl { get; set; }
    [MaxLength(50)]
    public string? Source { get; set; }
    
}
