using Core.Entities.EntityDesign;

namespace Share.Models.EntityModelDtos;
/// <summary>
/// 实体模型类添加时请求结构
/// </summary>
public class EntityModelAddDto
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

    /// <summary>
    /// 代码示例
    /// </summary>
    [MaxLength(2000)]
    public string? CodeExample { get; set; }
    public CodeLanguage? CodeLanguage { get; set; }
    public Guid ParentEntityId { get; set; } = default!;
    public Guid EntityLibraryId { get; set; } = default!;
    
}
