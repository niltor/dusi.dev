using Application.IManager;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Share.Models.ThirdNewsDtos;

namespace Application.Manager;

public class ThirdNewsManager : DomainManagerBase<ThirdNews, ThirdNewsUpdateDto, ThirdNewsFilterDto, ThirdNewsItemDto>, IThirdNewsManager
{

    private readonly IUserContext _userContext;
    public ThirdNewsManager(DataStoreContext storeContext, IUserContext userContext) : base(storeContext)
    {
        _userContext = userContext;
    }

    /// <summary>
    /// 创建待添加实体
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<ThirdNews> CreateNewEntityAsync(ThirdNewsAddDto dto)
    {
        var entity = dto.MapTo<ThirdNewsAddDto, ThirdNews>();
        // 构建实体
        return Task.FromResult(entity);
    }

    public override async Task<ThirdNews> UpdateAsync(ThirdNews entity, ThirdNewsUpdateDto dto)
    {
        // 根据实际业务更新
        return await base.UpdateAsync(entity, dto);
    }

    public override async Task<PageList<ThirdNewsItemDto>> FilterAsync(ThirdNewsFilterDto filter)
    {
        Queryable = Queryable.WhereNotNull(filter.AuthorName, q => q.AuthorName == filter.AuthorName);
        Queryable = Queryable.WhereNotNull(filter.Title, q => q.Title.StartsWith(filter.Title!));

        Queryable = Queryable.WhereNotNull(filter.Type, q => q.Type == filter.Type);
        Queryable = Queryable.WhereNotNull(filter.NewsType, q => q.NewsType == filter.NewsType);
        Queryable = Queryable.WhereNotNull(filter.TechType, q => q.TechType == filter.TechType);

        Queryable = Queryable.WhereNotNull(filter.NewsStatus, q => q.NewsStatus == filter.NewsStatus);

        if (filter.StartDate != null && filter.EndDate != null)
        {
            Queryable = Queryable.Where(q => q.DatePublished >= filter.StartDate && q.DatePublished < filter.EndDate);
        }

        return await Query.FilterAsync<ThirdNewsItemDto>(Queryable, filter.PageIndex, filter.PageSize);
    }

    /// <summary>
    /// 当前用户所拥有的对象
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ThirdNews?> GetOwnedAsync(Guid id)
    {
        var query = Command.Db.Where(q => q.Id == id);
        // TODO:获取用户所属的对象
        // query = query.Where(q => q.User.Id == _userContext.UserId);
        return await query.FirstOrDefaultAsync();
    }
}
