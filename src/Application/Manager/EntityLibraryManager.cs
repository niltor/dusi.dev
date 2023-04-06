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
        return await base.UpdateAsync(entity, dto);
    }

    public override async Task<PageList<EntityLibraryItemDto>> FilterAsync(EntityLibraryFilterDto filter)
    {
        Queryable = Queryable.WhereNotNull(filter.Name, q => q.Name.StartsWith(filter.Name!));
        return await Query.FilterAsync<EntityLibraryItemDto>(Queryable);
    }

}
