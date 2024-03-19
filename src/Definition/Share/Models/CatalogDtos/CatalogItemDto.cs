using Entity.CMS;
using Ater.Web.Core.Models;

namespace Share.Models.CatalogDtos;
/// <summary>
/// 目录列表元素
/// </summary>
/// <inheritdoc cref="Entity.CMS.Catalog"/>
public class CatalogItemDto
{
    /// <summary>
    /// 目录名称
    /// </summary>
    [MaxLength(50)]
    public string Name { get; set; } = default!;
    /// <summary>
    /// 层级
    /// </summary>
    public short Level { get; set; } = 0;
    public Guid? ParentId { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.UtcNow;
}
