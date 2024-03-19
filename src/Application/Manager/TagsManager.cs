using Share.Models.TagsDtos;

namespace Application.Manager;

public class TagsManager(DataAccessContext<Tags> storeContext, IUserContext userContext, ILogger<TagsManager> logger) : ManagerBase<Tags, TagsUpdateDto, TagsFilterDto, TagsItemDto>(storeContext, logger)
{

    private readonly IUserContext _userContext = userContext;

    /// <summary>
    /// 创建待添加实体
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<Tags> CreateNewEntityAsync(TagsAddDto dto)
    {
        Tags entity = dto.MapTo<TagsAddDto, Tags>();
        User user = await _userContext.GetUserAsync() ?? throw new Exception(Const.ErrorMsg.NotFoundUser);
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
        User? user = await _userContext.GetUserAsync();
        List<string> currentTagNames = await Query.Db.Where(t => t.User.Id == _userContext.UserId)
            .Select(t => t.Name).ToListAsync();

        List<Tags> newTags = list.Distinct()
            .Where(l => !currentTagNames.Contains(l.Name))
            .Select(t => new Tags
            {
                Name = t.Name,
                Color = t.Color,
                User = user!
            })
            .ToList();

        Command.Db.AddRange(newTags);
        return await Command.SaveChangesAsync();
    }

    public override async Task<Tags> UpdateAsync(Tags entity, TagsUpdateDto dto)
    {
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
        IQueryable<Tags> query = Command.Db.Where(q => q.Id == id);
        query = query.Where(q => q.User.Id == _userContext.UserId);
        return await query.FirstOrDefaultAsync();
    }
}
