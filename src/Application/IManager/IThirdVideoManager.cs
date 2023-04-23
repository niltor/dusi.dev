using Share.Models.ThirdVideoDtos;

namespace Application.IManager;
/// <summary>
/// 定义实体业务接口规范
/// </summary>
public interface IThirdVideoManager : IDomainManager<ThirdVideo>
{
    /// <summary>
    /// 当前用户所拥有的对象
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ThirdVideo?> GetOwnedAsync(Guid id);

    /// <summary>
    /// 创建待添加实体
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<ThirdVideo> CreateNewEntityAsync(ThirdVideoAddDto dto);
    Task<ThirdVideo?> GetCurrentAsync(Guid id, params string[] navigations);
    Task<ThirdVideo> AddAsync(ThirdVideo entity);
    Task<ThirdVideo> UpdateAsync(ThirdVideo entity, ThirdVideoUpdateDto dto);
    Task<ThirdVideo?> FindAsync(Guid id);
    Task<TDto?> FindAsync<TDto>(Expression<Func<ThirdVideo, bool>>? whereExp) where TDto : class;
    Task<List<TDto>> ListAsync<TDto>(Expression<Func<ThirdVideo, bool>>? whereExp) where TDto : class;
    Task<PageList<ThirdVideoItemDto>> FilterAsync(ThirdVideoFilterDto filter);
    Task<ThirdVideo?> DeleteAsync(ThirdVideo entity, bool softDelete = true);
    Task<bool> ExistAsync(Guid id);
}
