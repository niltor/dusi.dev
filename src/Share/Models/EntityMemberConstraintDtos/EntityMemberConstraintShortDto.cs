using Core.Entities.EntityDesign;

namespace Share.Models.EntityMemberConstraintDtos;
/// <summary>
/// 属性的约束概要
/// </summary>
public class EntityMemberConstraintShortDto
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
    public EntityMember EntityMember { get; set; } = default!;
    public Guid EntityMemberId { get; set; } = default!;
    public Guid Id { get; set; } = default!;
    public DateTimeOffset CreatedTime { get; set; } = default!;
    public DateTimeOffset UpdatedTime { get; set; } = default!;
    /// <summary>
    /// 软删除
    /// </summary>
    public bool IsDeleted { get; set; } = default!;
    
}
