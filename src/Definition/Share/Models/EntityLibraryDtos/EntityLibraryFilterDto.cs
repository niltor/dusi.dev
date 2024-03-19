using Entity.EntityDesign;
namespace Share.Models.EntityLibraryDtos;
/// <summary>
/// 实体库查询筛选
/// </summary>
/// <inheritdoc cref="Entity.EntityDesign.EntityLibrary"/>
public class EntityLibraryFilterDto : FilterBase
{
    /// <summary>
    /// 库名称
    /// </summary>
    [MaxLength(60)]
    public string? Name { get; set; }
}
