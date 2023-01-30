using Core.Entities.EntityDesign;
namespace Share.Models.EntityModelDtos;
/// <summary>
/// 实体模型类查询筛选
/// </summary>
/// <inheritdoc cref="Core.Entities.EntityDesign.EntityModel"/>
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
    /// <summary>
    /// 代码语言
    /// </summary>
    public CodeLanguage? CodeLanguage { get; set; }
    /// <summary>
    /// 语言版本
    /// </summary>
    public string? LanguageVersion { get; set; }
    public Guid? ParentEntityId { get; set; }
    public Guid? EntityLibraryId { get; set; }
    
}
