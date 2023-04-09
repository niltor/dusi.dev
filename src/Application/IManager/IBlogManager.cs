using Share.Models.BlogDtos;

namespace Application.IManager;
/// <summary>
/// 定义实体业务接口规范
/// </summary>
public interface IBlogManager : IDomainManager<Blog, BlogUpdateDto, BlogFilterDto, BlogItemDto>
{
	/// <summary>
    /// 当前用户所拥有的对象
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Blog?> GetOwnedAsync(Guid id);

    List<EnumDictionary> GetTypes();

    Task<Blog> CreateNewEntityAsync(BlogAddDto dto, User user, Catalog catalog);
    // TODO: 定义业务方法
}
