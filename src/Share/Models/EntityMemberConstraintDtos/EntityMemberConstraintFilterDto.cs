namespace Share.Models.EntityMemberConstraintDtos;
/// <summary>
/// 属性的约束查询筛选
/// </summary>
public class EntityMemberConstraintFilterDto : FilterBase
{
    public Guid? EntityMemberId { get; set; }
    
}
