using Share.Models.EntityMemberConstraintDtos;

namespace Application.Manager;

public class EntityMemberConstraintManager : DomainManagerBase<EntityMemberConstraint, EntityMemberConstraintUpdateDto, EntityMemberConstraintFilterDto, EntityMemberConstraintItemDto>, IDomainManager<EntityMemberConstraint>
{
	public EntityMemberConstraintManager(DataStoreContext storeContext) : base(storeContext)
	{
	}

	public override async Task<EntityMemberConstraint> UpdateAsync(EntityMemberConstraint entity, EntityMemberConstraintUpdateDto dto)
	{
		// TODO:根据实际业务更新
		return await base.UpdateAsync(entity, dto);
	}

	public override async Task<PageList<EntityMemberConstraintItemDto>> FilterAsync(EntityMemberConstraintFilterDto filter)
	{
		// TODO:根据实际业务构建筛选条件
		// if ... Queryable = ...
		return await Query.FilterAsync<EntityMemberConstraintItemDto>(Queryable);
	}

}
