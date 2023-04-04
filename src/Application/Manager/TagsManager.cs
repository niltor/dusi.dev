using Application.IManager;
using Core.Const;
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
    public async Task<Tags> CreateNewEntityAsync(TagsAddDto dto)
    {
        var entity = dto.MapTo<TagsAddDto, Tags>();
        var user = await _userContext.GetUserAsync() ?? throw new Exception(ErrorMsg.NotFoundUser);
        entity.User = user!;
        return entity;
    }


    /// <summary>
    /// 批量添加
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public async Task<int> BatchAddAsync(List<TagsAddDto> list)
    {
        // 去重添加
        var user = await _userContext.GetUserAsync();
        var currentTagNames = await Query.Db.Where(t => t.User.Id == _userContext.UserId)
            .Select(t => t.Name).ToListAsync();

        var newTags = list.Distinct()
            .Where(l => !currentTagNames.Contains(l.Name))
            .Select(t => new Tags
            {
                Name = t.Name,
                Color = t.Color,
                User = user!
            })
            .ToList();

        Command.Db.AddRange(newTags);
        return await Command.SaveChangeAsync();
    }

    public override async Task<Tags> UpdateAsync(Tags entity, TagsUpdateDto dto)
    {
        // TODO:根据实际业务更新
        return await base.UpdateAsync(entity, dto);
    }

    public override async Task<PageList<TagsItemDto>> FilterAsync(TagsFilterDto filter)
    {
        Queryable = Queryable.Where(q => q.User.Id == _userContext.UserId);
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
