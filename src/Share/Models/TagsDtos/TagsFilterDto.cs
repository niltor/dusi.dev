using Entity.CMS;
namespace Share.Models.TagsDtos;
/// <summary>
/// 标签查询筛选
/// </summary>
/// <inheritdoc cref="Entity.CMS.Tags"/>
public class TagsFilterDto : FilterBase
{
    /// <summary>
    /// 标签名称
    /// </summary>
    [MaxLength(50)]
    public string? Name { get; set; }
    /// <summary>
    /// 标签颜色
    /// </summary>
    [MaxLength(20)]
    public string? Color { get; set; }
    public Guid? UserId { get; set; }
}
