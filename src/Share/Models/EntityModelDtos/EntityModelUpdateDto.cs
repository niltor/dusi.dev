using Entity.EntityDesign;
namespace Share.Models.EntityModelDtos;
/// <summary>
/// 实体模型类更新时请求结构
/// </summary>
/// <inheritdoc cref="Entity.EntityDesign.EntityModel"/>
public class EntityModelUpdateDto
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
    /// 代码内容
    /// </summary>
    [MaxLength(8000)]
    public string? CodeContent { get; set; }
    /// <summary>
    /// 代码语言
    /// </summary>
    public CodeLanguage? CodeLanguage { get; set; }
    /// <summary>
    /// 语言版本
    /// </summary>
    [MaxLength(20)]
    public string? LanguageVersion { get; set; }
    public Guid? EntityLibraryId { get; set; }
}
