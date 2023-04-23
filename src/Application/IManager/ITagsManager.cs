using Share.Models.TagsDtos;

namespace Application.IManager;
/// <summary>
/// 定义实体业务接口规范
/// </summary>
public interface ITagsManager : IDomainManager<Tags>
{
    /// <summary>
    /// 当前用户所拥有的对象
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Tags?> GetOwnedAsync(Guid id);

    /// <summary>
    /// 创建待添加实体
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<Tags> CreateNewEntityAsync(TagsAddDto dto);
    Task<int> BatchAddAsync(List<TagsAddDto> list);
    Task<Tags?> GetCurrentAsync(Guid id, params string[] navigations);
    Task<Tags> AddAsync(Tags entity);
    Task<Tags> UpdateAsync(Tags entity, TagsUpdateDto dto);
    Task<Tags?> FindAsync(Guid id);
    Task<TDto?> FindAsync<TDto>(Expression<Func<Tags, bool>>? whereExp) where TDto : class;
    Task<List<TDto>> ListAsync<TDto>(Expression<Func<Tags, bool>>? whereExp) where TDto : class;
    Task<PageList<TagsItemDto>> FilterAsync(TagsFilterDto filter);
    Task<Tags?> DeleteAsync(Tags entity, bool softDelete = true);
    Task<bool> ExistAsync(Guid id);
    // TODO: 定义业务方法
}
