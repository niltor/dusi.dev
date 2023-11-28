using Entity.CMS;
namespace Share.Models.TagsDtos;
/// <summary>
/// 标签添加时请求结构
/// </summary>
/// <inheritdoc cref="Entity.CMS.Tags"/>
public class TagsAddDto
{
    /// <summary>
    /// 标签名称
    /// </summary>
    [MaxLength(50)]
    public required string Name { get; set; }
    /// <summary>
    /// 标签颜色
    /// </summary>
    [MaxLength(20)]
    public string? Color { get; set; }
}
