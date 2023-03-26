using Core.Utils;
namespace Share.Models.ThirdNewsDtos;

/// <summary>
/// 枚举类型选项
/// </summary>
public class ThirdNewsOptionsDto
{
    public List<EnumDictionary>? NewsType { get; set; }
    public List<EnumDictionary>? TechType { get; set; }
}
