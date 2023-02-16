namespace Core.Entities.EntityDesign;
/// <summary>
/// 实体库
/// </summary>
[NgPage("system", "entityLibrary")]
public class EntityLibrary : EntityBase
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
    public bool IsPublic { get; set; } = true;

    /// <summary>
    /// 包含的实体类
    /// </summary>
    public List<EntityModel>? EntityModels { get; set; }

    /// <summary>
    /// 所属用户
    /// </summary>
    public required User User { get; set; }
}
