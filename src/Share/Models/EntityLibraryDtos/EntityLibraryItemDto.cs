using Core.Entities.EntityDesign;
namespace Share.Models.EntityLibraryDtos;
/// <summary>
/// 实体库列表元素
/// </summary>
/// <inheritdoc cref="Core.Entities.EntityDesign.EntityLibrary"/>
public class EntityLibraryItemDto
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
    public bool IsPublic { get; set; }
    public Guid Id { get; set; }
    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset UpdatedTime { get; set; }
    
}
