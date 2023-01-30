using Core.Entities.EntityDesign;
namespace Share.Models.EntityLibraryDtos;
/// <summary>
/// 实体库更新时请求结构
/// </summary>
/// <inheritdoc cref="Core.Entities.EntityDesign.EntityLibrary"/>
public class EntityLibraryUpdateDto
{
    /// <summary>
    /// 库名称
    /// </summary>
    [MaxLength(60)]
    public required string Name { get; set; }
    /// <summary>
    /// 库描述
    /// </summary>
    [MaxLength(200)]
    public string? Description { get; set; }
    /// <summary>
    /// 是否公开
    /// </summary>
    public bool? IsPublic { get; set; }
    /// <summary>
    /// 包含的实体类
    /// </summary>
    public List<EntityModel>? EntityModels { get; set; }
    /// <summary>
    /// 所属用户
    /// </summary>
    public required SystemUser User { get; set; }
    public required Guid UserId { get; set; }
    
}
