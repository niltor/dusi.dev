using Share.Models.ThirdNewsDtos;

namespace Application.Manager;

public class ThirdNewsManager : DomainManagerBase<ThirdNews, ThirdNewsUpdateDto, ThirdNewsFilterDto, ThirdNewsItemDto>, IThirdNewsManager
{

    private new readonly IUserContext _userContext;
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
        ThirdNews entity = dto.MapTo<ThirdNewsAddDto, ThirdNews>();
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

        // 未被分类的内容
        if (filter.IsClassified != null)
        {
            Queryable = filter.IsClassified.Value
                ? Queryable.Where(q =>
                    q.NewsType != NewsType.Default &&
                    q.TechType != TechType.Default &&
                    q.NewsStatus != NewsStatus.Default)
                : Queryable.Where(q =>
                    q.NewsType == NewsType.Default ||
                    q.TechType == TechType.Default ||
                    q.NewsStatus == NewsStatus.Default);
        }
        // 仅本周
        if (filter.OnlyWeek != null && filter.OnlyWeek.Value)
        {
            filter.EndDate = DateTimeOffset.UtcNow;
            int week = Convert.ToInt32(filter.EndDate.Value.DayOfWeek);
            week = week == 0 ? 7 : week;
            filter.StartDate = filter.EndDate.Value.AddDays(1 - week);
        }

        // 时间范围筛选
        if (filter.StartDate != null && filter.EndDate != null)
        {
            Queryable = Queryable.Where(q => q.CreatedTime >= filter.StartDate && q.CreatedTime < filter.EndDate);
        }

        return await Query.FilterAsync<ThirdNewsItemDto>(Queryable, filter.PageIndex, filter.PageSize, filter.OrderBy);
    }

    /// <summary>
    /// 批量操作
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<bool> BatchUpdateAsync(ThirdNewsBatchUpdateDto dto)
    {
        IQueryable<ThirdNews> query = Command.Db.Where(n => dto.Ids.Contains(n.Id)).AsQueryable();
        // 删除
        if (dto.IsDelete is not null and true)
        {
            return await query.ExecuteUpdateAsync(p => p
               .SetProperty(n => n.IsDeleted, true)) > 0;
        }
        // 标记
        if (dto.TechType != null)
        {
            return await query.ExecuteUpdateAsync(p => p
                .SetProperty(n => n.TechType, dto.TechType)
                .SetProperty(n => n.NewsStatus, NewsStatus.Public)) > 0;
        }
        return dto.NewsType != null
            ? await query.ExecuteUpdateAsync(p => p
                .SetProperty(n => n.NewsType, dto.NewsType)
                .SetProperty(n => n.NewsStatus, NewsStatus.Public)) > 0
            : dto.NewsStatus != null
&& await query.ExecuteUpdateAsync(p => p
                .SetProperty(n => n.NewsStatus, dto.NewsStatus)) > 0;
    }

    /// <summary>
    /// 当前用户所拥有的对象
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ThirdNews?> GetOwnedAsync(Guid id)
    {
        IQueryable<ThirdNews> query = Command.Db.Where(q => q.Id == id);
        return await query.FirstOrDefaultAsync();
    }

    /// <summary>
    /// 获取枚举选项
    /// </summary>
    /// <returns></returns>
    public ThirdNewsOptionsDto GetEnumOptions()
    {
        ThirdNewsOptionsDto res = new()
        {
            TechType = EnumHelper.ToList(typeof(TechType)),
            NewsType = EnumHelper.ToList(typeof(NewsType))
        };
        return res;
    }

}
