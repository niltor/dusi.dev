using Core.Entities.EntityDesign;

namespace Share.Models.EntityModelDtos;
/// <summary>
/// 实体模型类查询筛选
/// </summary>
public class EntityModelFilterDto : FilterBase
{
    /// <summary>
    /// 实体类名
    /// </summary>
    [MaxLength(60)]
    public string? Name { get; set; }

    /// <summary>
    /// 实体注释内容
    /// </summary>
    [MaxLength(300)]
    public string? Comment { get; set; }

    /// <summary>
    /// 访问修饰符
    /// </summary>
    public AccessModifier? AccessModifier { get; set; }
    public Guid? ParentEntityId { get; set; } = default!;
    public Guid? EntityLibraryId { get; set; } = default!;
    
}
