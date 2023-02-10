using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.CMS;
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
