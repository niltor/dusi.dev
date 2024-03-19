using Share.Models.EntityLibraryDtos;

namespace Application.Manager;

public class EntityLibraryManager(DataAccessContext<EntityLibrary> storeContext, ILogger<EntityLibraryManager> logger) : ManagerBase<EntityLibrary, EntityLibraryUpdateDto, EntityLibraryFilterDto, EntityLibraryItemDto>(storeContext, logger)
{
    public override async Task<EntityLibrary> UpdateAsync(EntityLibrary entity, EntityLibraryUpdateDto dto)
    {
        return await base.UpdateAsync(entity, dto);
    }

    public override async Task<PageList<EntityLibraryItemDto>> FilterAsync(EntityLibraryFilterDto filter)
    {
        Queryable = Queryable.WhereNotNull(filter.Name, q => q.Name.StartsWith(filter.Name!));
        return await Query.FilterAsync<EntityLibraryItemDto>(Queryable);
    }

}
