using Share.Models.ThirdNewsDtos;

namespace Application.IManager;
/// <summary>
/// 定义实体业务接口规范
/// </summary>
public interface IThirdNewsManager : IDomainManager<ThirdNews>
{
    /// <summary>
    /// 当前用户所拥有的对象
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ThirdNews?> GetOwnedAsync(Guid id);

    /// <summary>
    /// 创建待添加实体
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<ThirdNews> CreateNewEntityAsync(ThirdNewsAddDto dto);
    Task<bool> BatchUpdateAsync(ThirdNewsBatchUpdateDto dto);
    ThirdNewsOptionsDto GetEnumOptions();
    Task<ThirdNews?> GetCurrentAsync(Guid id, params string[] navigations);
    Task<ThirdNews> AddAsync(ThirdNews entity);
    Task<ThirdNews> UpdateAsync(ThirdNews entity, ThirdNewsUpdateDto dto);
    Task<ThirdNews?> FindAsync(Guid id);
    Task<TDto?> FindAsync<TDto>(Expression<Func<ThirdNews, bool>>? whereExp) where TDto : class;
    Task<List<TDto>> ListAsync<TDto>(Expression<Func<ThirdNews, bool>>? whereExp) where TDto : class;
    Task<PageList<ThirdNewsItemDto>> FilterAsync(ThirdNewsFilterDto filter);
    Task<ThirdNews?> DeleteAsync(ThirdNews entity, bool softDelete = true);
    Task<bool> ExistAsync(Guid id);
}
