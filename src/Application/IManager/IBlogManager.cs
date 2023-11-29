using Share.Models.BlogDtos;

namespace Application.IManager;
/// <summary>
/// 定义实体业务接口规范
/// </summary>
public interface IBlogManager : IDomainManager<Blog>
{
    /// <summary>
    /// 当前用户所拥有的对象
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Blog?> GetOwnedAsync(Guid id);

    List<EnumDictionary> GetTypes();

    Task<Blog> CreateNewEntityAsync(BlogAddDto dto);
    Task<Blog?> GetCurrentAsync(Guid id, params string[] navigations);
    Task<Blog> AddAsync(Blog entity);
    Task<Blog> UpdateAsync(Blog entity, BlogUpdateDto dto);
    Task<Blog?> FindAsync(Guid id);
    Task<TDto?> FindAsync<TDto>(Expression<Func<Blog, bool>>? whereExp) where TDto : class;
    Task<List<TDto>> ListAsync<TDto>(Expression<Func<Blog, bool>>? whereExp) where TDto : class;
    Task<PageList<BlogItemDto>> FilterAsync(BlogFilterDto filter);
    Task<Blog?> DeleteAsync(Blog entity, bool softDelete = true);
    Task<bool> ExistAsync(Guid id);
    // TODO: 定义业务方法
}
