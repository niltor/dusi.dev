using Share.Models.ThirdVideoDtos;

namespace Application.Manager;

public class ThirdVideoManager : DomainManagerBase<ThirdVideo, ThirdVideoUpdateDto, ThirdVideoFilterDto, ThirdVideoItemDto>, IDomainManager<ThirdVideo>
{

	public ThirdVideoManager(DataStoreContext storeContext, IUserContext userContext) : base(storeContext)
	{
		_userContext = userContext;
	}

	/// <summary>
	/// 创建待添加实体
	/// </summary>
	/// <param name="dto"></param>
	/// <returns></returns>
	public Task<ThirdVideo> CreateNewEntityAsync(ThirdVideoAddDto dto)
	{
		ThirdVideo entity = dto.MapTo<ThirdVideoAddDto, ThirdVideo>();
		// 构建实体
		return Task.FromResult(entity);
	}

	public override async Task<ThirdVideo> UpdateAsync(ThirdVideo entity, ThirdVideoUpdateDto dto)
	{
		// 根据实际业务更新
		return await base.UpdateAsync(entity, dto);
	}

	public override async Task<PageList<ThirdVideoItemDto>> FilterAsync(ThirdVideoFilterDto filter)
	{
		// TODO:根据实际业务构建筛选条件
		// if (...){ Queryable = ...;}
		return await Query.FilterAsync<ThirdVideoItemDto>(Queryable, filter.PageIndex, filter.PageSize);
	}

	/// <summary>
	/// 当前用户所拥有的对象
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public async Task<ThirdVideo?> GetOwnedAsync(Guid id)
	{
		IQueryable<ThirdVideo> query = Command.Db.Where(q => q.Id == id);
		// TODO:获取用户所属的对象
		// query = query.Where(q => q.User.Id == _userContext.UserId);
		return await query.FirstOrDefaultAsync();
	}
}
