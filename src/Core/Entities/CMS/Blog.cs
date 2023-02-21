namespace Core.Entities.CMS;
/// <summary>
/// 博客
/// </summary>
public class Blog : TextBase
{
    /// <summary>
    /// 标题
    /// </summary>
    public string? TranslateTitle { get; set; }
    /// <summary>
    /// 翻译内容
    /// </summary>
    [MaxLength(12000)]
    public string? TranslateContent { get; set; }
    /// <summary>
    /// 语言类型
    /// </summary>
    public LanguageType LanguageType { get; set; } = LanguageType.CN;
    /// <summary>
    /// 是否公开
    /// </summary>
    public bool IsPublic { get; set; } = true;
    /// <summary>
    /// 是否原创
    /// </summary>
    public bool IsOriginal { get; set; }
    public required User User { get; set; }
    /// <summary>
    /// 所属目录
    /// </summary>
    public required Catalog Catalog { get; set; }
    public List<Tags>? Tags { get; set; }
}

public enum LanguageType
{
    /// <summary>
    /// 中文
    /// </summary>
    CN,
    /// <summary>
    /// 英文
    /// </summary>
    EN
}
