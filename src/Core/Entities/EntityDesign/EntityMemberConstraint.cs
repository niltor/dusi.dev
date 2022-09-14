using Core.Entities.Codes;

namespace Core.Entities.EntityDesign;
/// <summary>
/// 属性的约束
/// </summary>
public class EntityMemberConstraint
{
    /// <summary>
    /// 字符串最长长度
    /// </summary>
    public int? MaxLength { get; set; }
    /// <summary>
    /// 字符串最短长度
    /// </summary>
    public int? MinLength { get; set; }
    /// <summary>
    /// 固定长度
    /// </summary>
    public int? Length { get; set; }
    /// <summary>
    /// 数值最小
    /// </summary>
    public int? Min { get; set; }
    /// <summary>
    /// 数值最大
    /// </summary>
    public long? Max { get; set; }

    /// <summary>
    /// 所属属性
    /// </summary>
    public required EntityMember EntityMember { get; set; }

}
