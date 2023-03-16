using Application.IManager;
using Share.Models.TagsDtos;

namespace Application.Manager;

public class TagsManager : DomainManagerBase<Tags, TagsUpdateDto, TagsFilterDto, TagsItemDto>, ITagsManager
{

    private readonly IUserContext _userContext;
    public TagsManager(DataStoreContext storeContext, IUserContext userContext) : base(storeContext)
    {
        _userContext = userContext;
    }

    /// <summary>
    /// 创建待添加实体
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<Tags> CreateNewEntityAsync(TagsAddDto dto)
    {
        var entity = dto.MapTo<TagsAddDto, Tags>();
        // TODO:构建实体
        return Task.FromResult(entity);
    }

    public override async Task<Tags> UpdateAsync(Tags entity, TagsUpdateDto dto)
    {
        // TODO:根据实际业务更新
        return await base.UpdateAsync(entity, dto);
    }

    public override async Task<PageList<TagsItemDto>> FilterAsync(TagsFilterDto filter)
    {
        // TODO:根据实际业务构建筛选条件
        // if ... Queryable = ...
        return await Query.FilterAsync<TagsItemDto>(Queryable, filter.PageIndex, filter.PageSize);
    }

    /// <summary>
    /// 当前用户所拥有的对象
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Tags?> GetOwnedAsync(Guid id)
    {
        var query = Command.Db.Where(q => q.Id == id);
        query = query.Where(q => q.User.Id == _userContext.UserId);
        return await query.FirstOrDefaultAsync();
    }
}
