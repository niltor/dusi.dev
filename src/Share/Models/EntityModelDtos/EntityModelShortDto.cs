using Core.Entities.EntityDesign;
namespace Share.Models.EntityModelDtos;
/// <summary>
/// 实体模型类概要
/// </summary>
/// <inheritdoc cref="Core.Entities.EntityDesign.EntityModel"/>
public class EntityModelShortDto
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
    /// 所属模型库
    /// </summary>
    public EntityLibrary EntityLibrary { get; set; } = default!;
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedTime { get; set; } = DateTimeOffset.UtcNow;
    
}
