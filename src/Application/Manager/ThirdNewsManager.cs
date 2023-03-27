using Application.IManager;
using Core.Utils;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Share.Models;
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

        // 未被分类的内容
        if (filter.IsClassified != null)
        {
            if (filter.IsClassified.Value)
            {
                Queryable = Queryable.Where(q =>
                    q.NewsType != NewsType.Default &&
                    q.TechType != TechType.Default &&
                    q.NewsStatus != NewsStatus.Default);
            }
            else
            {
                Queryable = Queryable.Where(q =>
                    q.NewsType == NewsType.Default ||
                    q.TechType == TechType.Default ||
                    q.NewsStatus == NewsStatus.Default);
            }
        }

        if (filter.StartDate != null && filter.EndDate != null)
        {
            Queryable = Queryable.Where(q => q.DatePublished >= filter.StartDate && q.DatePublished < filter.EndDate);
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
        var query = Command.Db.Where(n => dto.Ids.Contains(n.Id)).AsQueryable();
        // 删除
        if (dto.IsDelete != null && dto.IsDelete == true)
        {
            return await query.ExecuteDeleteAsync() > 0;
        }
        // 标记
        if (dto.TechType != null)
        {
            return await query.ExecuteUpdateAsync(p => p
                .SetProperty(n => n.TechType, dto.TechType)
                .SetProperty(n => n.NewsStatus, NewsStatus.Public)) > 0;
        }
        if (dto.NewsType != null)
        {
            return await query.ExecuteUpdateAsync(p => p
                .SetProperty(n => n.NewsType, dto.NewsType)
                .SetProperty(n => n.NewsStatus, NewsStatus.Public)) > 0;
        }
        if (dto.NewsStatus != null)
        {
            return await query.ExecuteUpdateAsync(p => p
                .SetProperty(n => n.NewsStatus, dto.NewsStatus)) > 0;
        }
        return false;
    }

    /// <summary>
    /// 当前用户所拥有的对象
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ThirdNews?> GetOwnedAsync(Guid id)
    {
        var query = Command.Db.Where(q => q.Id == id);
        return await query.FirstOrDefaultAsync();
    }

    /// <summary>
    /// 获取枚举选项
    /// </summary>
    /// <returns></returns>
    public ThirdNewsOptionsDto GetEnumOptions()
    {
        var res = new ThirdNewsOptionsDto
        {
            TechType = EnumHelper.ToList(typeof(TechType)),
            NewsType = EnumHelper.ToList(typeof(NewsType))
        };
        return res;
    }
}
