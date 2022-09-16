using Application.IManager;
using Share.Models.EntityLibraryDtos;

namespace Application.Manager;

public class EntityLibraryManager : DomainManagerBase<EntityLibrary, EntityLibraryUpdateDto, EntityLibraryFilterDto, EntityLibraryItemDto>, IEntityLibraryManager
{
    public EntityLibraryManager(DataStoreContext storeContext) : base(storeContext)
    {
    }

    public override async Task<EntityLibrary> UpdateAsync(EntityLibrary entity, EntityLibraryUpdateDto dto)
    {
        // TODO:根据实际业务更新
        return await base.UpdateAsync(entity, dto);
    }

    public override async Task<PageList<EntityLibraryItemDto>> FilterAsync(EntityLibraryFilterDto filter)
    {
        // TODO:根据实际业务构建筛选条件
        // if ... Queryable = ...
        return await Query.FilterAsync<EntityLibraryItemDto>(Queryable);
    }

}
