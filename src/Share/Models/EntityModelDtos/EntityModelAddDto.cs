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
    [MaxLength(20)]
    public string LanguageVersion { get; set; } = "latest";
    /// <summary>
    /// 所属实体库
    /// </summary>
    public required Guid EntityLibraryId { get; set; }

}
