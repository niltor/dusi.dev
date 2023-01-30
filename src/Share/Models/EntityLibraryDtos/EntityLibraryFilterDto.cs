using Core.Entities.EntityDesign;
namespace Share.Models.EntityLibraryDtos;
/// <summary>
/// 实体库查询筛选
/// </summary>
/// <inheritdoc cref="Core.Entities.EntityDesign.EntityLibrary"/>
public class EntityLibraryFilterDto : FilterBase
{
    /// <summary>
    /// 库名称
    /// </summary>
    [MaxLength(60)]
    public string? Name { get; set; }
    /// <summary>
    /// 库描述
    /// </summary>
    [MaxLength(200)]
    public string? Description { get; set; }
    /// <summary>
    /// 是否公开
    /// </summary>
    public bool? IsPublic { get; set; }
    public Guid? UserId { get; set; }
    
}
