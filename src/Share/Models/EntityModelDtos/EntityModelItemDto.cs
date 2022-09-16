using Core.Entities.EntityDesign;

namespace Share.Models.EntityModelDtos;
/// <summary>
/// 实体模型类列表元素
/// </summary>
public class EntityModelItemDto
{
    /// <summary>
    /// 实体类名
    /// </summary>
    [MaxLength(60)]
    public string Name { get; set; } = default!;

    /// <summary>
    /// 实体注释内容
    /// </summary>
    [MaxLength(300)]
    public string Comment { get; set; } = default!;

    /// <summary>
    /// 访问修饰符
    /// </summary>
    public AccessModifier AccessModifier { get; set; } = default!;
    public CodeLanguage? CodeLanguage { get; set; }
    public Guid Id { get; set; } = default!;
    public DateTimeOffset CreatedTime { get; set; } = default!;
    public DateTimeOffset UpdatedTime { get; set; } = default!;
    /// <summary>
    /// 软删除
    /// </summary>
    public bool IsDeleted { get; set; } = default!;
    
}
