using Application.IManager;
using Share.Models.EntityModelDtos;

namespace Application.Manager;

public class EntityModelManager : DomainManagerBase<EntityModel, EntityModelUpdateDto, EntityModelFilterDto, EntityModelItemDto>, IEntityModelManager
{
    public EntityModelManager(DataStoreContext storeContext) : base(storeContext)
    {
    }

    public override async Task<EntityModel> UpdateAsync(EntityModel entity, EntityModelUpdateDto dto)
    {
        // TODO:根据实际业务更新
        return await base.UpdateAsync(entity, dto);
    }

    public override async Task<PageList<EntityModelItemDto>> FilterAsync(EntityModelFilterDto filter)
    {
        // TODO:根据实际业务构建筛选条件
        // if ... Queryable = ...
        return await Query.FilterAsync<EntityModelItemDto>(Queryable);
    }

}
