using Core.Entities.EntityDesign;
namespace Share.Models.EntityModelDtos;
/// <summary>
/// 实体模型类添加时请求结构
/// </summary>
/// <inheritdoc cref="Core.Entities.EntityDesign.EntityModel"/>
public class EntityModelAddDto
{
    /// <summary>
    /// 实体类名
    /// </summary>
    [MaxLength(60)]
    public required string Name { get; set; }
    /// <summary>
    /// 实体注释内容
    /// </summary>
    [MaxLength(300)]
    public required string Comment { get; set; }
    /// <summary>
    /// 访问修饰符
    /// </summary>
    public required AccessModifier AccessModifier { get; set; } = AccessModifier.Public;
    /// <summary>
    /// 代码内容
    /// </summary>
    [MaxLength(8000)]
    public string? CodeContent { get; set; }
    /// <summary>
    /// 代码语言
    /// </summary>
    public CodeLanguage CodeLanguage { get; set; } = CodeLanguage.Csharp;
    /// <summary>
    /// 语言版本
    /// </summary>
    public string LanguageVersion { get; set; } = "latest";
    /// <summary>
    /// 父类
    /// </summary>
    public EntityModel? ParentEntity { get; set; }
    /// <summary>
    /// 直属子类
    /// </summary>
    public List<EntityModel>? ChildrenEntities { get; set; }
    /// <summary>
    /// 包含的属性
    /// </summary>
    public List<EntityMember>? EntityMembers { get; set; }
    /// <summary>
    /// 所属模型库
    /// </summary>
    public required EntityLibrary EntityLibrary { get; set; }
    public Guid ParentEntityId { get; set; }
    public Guid EntityLibraryId { get; set; }
    
}
