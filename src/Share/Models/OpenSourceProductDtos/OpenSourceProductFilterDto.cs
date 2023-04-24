using Core.Entities.CMS;
namespace Share.Models.OpenSourceProductDtos;
/// <summary>
/// 开源作品查询筛选
/// </summary>
/// <inheritdoc cref="Core.Entities.CMS.OpenSourceProduct"/>
public class OpenSourceProductFilterDto : FilterBase
{
    /// <summary>
    /// 标题
    /// </summary>
    [MaxLength(100)]
    public string? Title { get; set; }
    /// <summary>
    /// 标签
    /// </summary>
    [MaxLength(300)]
    public string? Tags { get; set; }
    
}
