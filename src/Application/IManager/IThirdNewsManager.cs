using Share.Models.ThirdNewsDtos;

namespace Application.IManager;
/// <summary>
/// 定义实体业务接口规范
/// </summary>
public interface IThirdNewsManager : IDomainManager<ThirdNews, ThirdNewsUpdateDto, ThirdNewsFilterDto, ThirdNewsItemDto>
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
}
