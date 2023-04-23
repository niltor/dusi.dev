using Share.Models.EntityLibraryDtos;

namespace Application.IManager;
/// <summary>
/// 定义实体业务接口规范
/// </summary>
public interface IEntityLibraryManager : IDomainManager<EntityLibrary>
{
    Task<EntityLibrary?> GetCurrentAsync(Guid id, params string[] navigations);
    Task<EntityLibrary> AddAsync(EntityLibrary entity);
    Task<EntityLibrary> UpdateAsync(EntityLibrary entity, EntityLibraryUpdateDto dto);
    Task<EntityLibrary?> FindAsync(Guid id);
    Task<TDto?> FindAsync<TDto>(Expression<Func<EntityLibrary, bool>>? whereExp) where TDto : class;
    Task<List<TDto>> ListAsync<TDto>(Expression<Func<EntityLibrary, bool>>? whereExp) where TDto : class;
    Task<PageList<EntityLibraryItemDto>> FilterAsync(EntityLibraryFilterDto filter);
    Task<EntityLibrary?> DeleteAsync(EntityLibrary entity, bool softDelete = true);
    Task<bool> ExistAsync(Guid id);
    // TODO: 定义业务方法
}
