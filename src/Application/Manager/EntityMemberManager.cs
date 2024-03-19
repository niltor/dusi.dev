using Share.Models.EntityMemberDtos;

namespace Application.Manager;

public class EntityMemberManager(DataAccessContext<EntityMember> storeContext, ILogger<EntityMember> logger) : ManagerBase<EntityMember, EntityMemberUpdateDto, EntityMemberFilterDto, EntityMemberItemDto>(storeContext, logger)
{
    public override async Task<EntityMember> UpdateAsync(EntityMember entity, EntityMemberUpdateDto dto)
    {
        // TODO:根据实际业务更新
        return await base.UpdateAsync(entity, dto);
    }

    public override async Task<PageList<EntityMemberItemDto>> FilterAsync(EntityMemberFilterDto filter)
    {
        // TODO:根据实际业务构建筛选条件
        // if ... Queryable = ...
        return await Query.FilterAsync<EntityMemberItemDto>(Queryable);
    }

}
