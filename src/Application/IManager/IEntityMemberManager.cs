using Share.Models.EntityMemberDtos;

namespace Application.IManager;
/// <summary>
/// 定义实体业务接口规范
/// </summary>
public interface IEntityMemberManager : IDomainManager<EntityMember, EntityMemberUpdateDto, EntityMemberFilterDto, EntityMemberItemDto>
{
	// TODO: 定义业务方法
}
