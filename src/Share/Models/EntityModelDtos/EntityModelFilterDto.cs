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
    [MinLength(2)]
    public string? Name { get; set; }
    
    /// <summary>
    /// 代码语言
    /// </summary>
    public CodeLanguage? CodeLanguage { get; set; }
    /// <summary>
    /// 语言版本
    /// </summary>
    public string? LanguageVersion { get; set; }
    /// <summary>
    /// 所属实体库
    /// </summary>
    public Guid? EntityLibraryId { get; set; }
}
