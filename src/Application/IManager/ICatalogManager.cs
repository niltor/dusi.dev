using Share.Models.CatalogDtos;

namespace Application.IManager;
/// <summary>
/// 定义实体业务接口规范
/// </summary>
public interface ICatalogManager : IDomainManager<Catalog>
{
    /// <summary>
    /// 当前用户所拥有的对象
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Catalog?> GetOwnedAsync(Guid id);

    /// <summary>
    /// 创建待添加实体
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<Catalog> CreateNewEntityAsync(CatalogAddDto dto);
    Task<List<Catalog>> GetTreeAsync();
    Task<List<Catalog>> GetLeafCatalogsAsync();
    Task<Catalog?> GetCurrentAsync(Guid id, params string[] navigations);
    Task<Catalog> AddAsync(Catalog entity);
    Task<Catalog> UpdateAsync(Catalog entity, CatalogUpdateDto dto);
    Task<Catalog?> FindAsync(Guid id);
    Task<TDto?> FindAsync<TDto>(Expression<Func<Catalog, bool>>? whereExp) where TDto : class;
    Task<List<TDto>> ListAsync<TDto>(Expression<Func<Catalog, bool>>? whereExp) where TDto : class;
    Task<PageList<CatalogItemDto>> FilterAsync(CatalogFilterDto filter);
    Task<Catalog?> DeleteAsync(Catalog entity, bool softDelete = true);
    Task<bool> ExistAsync(Guid id);
}
