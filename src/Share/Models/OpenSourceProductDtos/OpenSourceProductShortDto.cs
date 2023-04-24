using Core.Entities.CMS;
namespace Share.Models.OpenSourceProductDtos;
/// <summary>
/// 开源作品概要
/// </summary>
/// <inheritdoc cref="Core.Entities.CMS.OpenSourceProduct"/>
public class OpenSourceProductShortDto
{
    /// <summary>
    /// 标题
    /// </summary>
    [MaxLength(100)]
    public string Title { get; set; } = default!;
    /// <summary>
    /// project url address
    /// </summary>
    [MaxLength(200)]
    public string ProjectUrl { get; set; } = default!;
    /// <summary>
    /// 描述
    /// </summary>
    [MaxLength(500)]
    public string Description { get; set; } = default!;
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
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedTime { get; set; } = DateTimeOffset.UtcNow;
    /// <summary>
    /// 所属用户
    /// </summary>
    public User? User { get; set; }
    
}
