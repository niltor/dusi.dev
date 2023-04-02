using Core.Entities.CMS;
namespace Share.Models.ThirdVideoDtos;
/// <summary>
/// 三方视频查询筛选
/// </summary>
/// <inheritdoc cref="Core.Entities.CMS.ThirdVideo"/>
public class ThirdVideoFilterDto : FilterBase
{
    [MaxLength(120)]
    public string? Title { get; set; }
}
