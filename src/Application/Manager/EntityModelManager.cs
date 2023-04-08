using Application.IManager;
using Share.Models.EntityModelDtos;

namespace Application.Manager;

public class EntityModelManager : DomainManagerBase<EntityModel, EntityModelUpdateDto, EntityModelFilterDto, EntityModelItemDto>, IEntityModelManager
{
    private readonly IEntityLibraryManager _libraryManager;

    public EntityModelManager(DataStoreContext storeContext, IEntityLibraryManager libraryManager) : base(storeContext)
    {
        _libraryManager = libraryManager;
    }

    public override async Task<EntityModel> UpdateAsync(EntityModel entity, EntityModelUpdateDto dto)
    {
        if (dto.EntityLibraryId != null)
        {
            var lib = await _libraryManager.GetCurrentAsync(dto.EntityLibraryId.Value);
            if (lib != null)
            {
                entity.EntityLibrary = lib;
            }
        }
        return await base.UpdateAsync(entity, dto);
    }

    public override async Task<PageList<EntityModelItemDto>> FilterAsync(EntityModelFilterDto filter)
    {
        Queryable = Queryable.WhereNotNull(filter.Name, q => q.Name.Contains(filter.Name!) || q.Comment.Contains(filter.Name!));
        Queryable = Queryable.WhereNotNull(filter.CodeLanguage, q => q.CodeLanguage == filter.CodeLanguage!);
        Queryable = Queryable.WhereNotNull(filter.LanguageVersion, q => q.LanguageVersion == filter.LanguageVersion!);
        Queryable = Queryable.WhereNotNull(filter.EntityLibraryId, q => q.EntityLibrary.Id == filter.EntityLibraryId!);

        return await Query.FilterAsync<EntityModelItemDto>(Queryable);
    }

    public override async Task<EntityModel?> FindAsync(Guid id)
    {
        return await Query.Db.Include(e => e.EntityLibrary)
            .SingleOrDefaultAsync(e => e.Id == id);
    }
}
