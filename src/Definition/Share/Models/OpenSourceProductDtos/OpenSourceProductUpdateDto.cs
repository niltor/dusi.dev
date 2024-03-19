using Entity.CMS;
namespace Share.Models.OpenSourceProductDtos;
/// <summary>
/// 开源作品更新时请求结构
/// </summary>
/// <inheritdoc cref="Entity.CMS.OpenSourceProduct"/>
public class OpenSourceProductUpdateDto
{
    /// <summary>
    /// 标题
    /// </summary>
    [MaxLength(100)]
    public string? Title { get; set; }
    /// <summary>
    /// project url address
    /// </summary>
    [MaxLength(200)]
    public string? ProjectUrl { get; set; }
    /// <summary>
    /// 描述
    /// </summary>
    [MaxLength(500)]
    public string? Description { get; set; }
    /// <summary>
    /// 缩略图
    /// </summary>
    [MaxLength(200)]
    public string? Thumbnail { get; set; }
    /// <summary>
    /// 作者
    /// </summary>
    [MaxLength(200)]
    public string? Authors { get; set; }
    /// <summary>
    /// 标签
    /// </summary>
    [MaxLength(300)]
    public string? Tags { get; set; }
    
}
