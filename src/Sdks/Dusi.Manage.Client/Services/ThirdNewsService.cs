using Core.Entities.CMS;
using Share.Models.ThirdNewsDtos;
namespace Dusi.Manage.Client.Services;
/// <summary>
/// 资讯管理
/// </summary>
public class ThirdNewsService : BaseService
{
    public ThirdNewsService(HttpClient httpClient) : base(httpClient)
    {

    }
    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="data">ThirdNewsFilterDto</param>
    /// <returns></returns>
    public async Task<PageList<ThirdNewsItemDto>?> FilterAsync(ThirdNewsFilterDto data)
    {
        string url = $"/api/admin/ThirdNews/filter";
        return await PostJsonAsync<PageList<ThirdNewsItemDto>?>(url, data);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="data">ThirdNewsAddDto</param>
    /// <returns></returns>
    public async Task<ThirdNews?> AddAsync(ThirdNewsAddDto data)
    {
        string url = $"/api/admin/ThirdNews";
        return await PostJsonAsync<ThirdNews?>(url, data);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"> </param>
    /// <param name="data">ThirdNewsUpdateDto</param>
    /// <returns></returns>
    public async Task<ThirdNews?> UpdateAsync(string id, ThirdNewsUpdateDto data)
    {
        string url = $"/api/admin/ThirdNews/{id}";
        return await PutJsonAsync<ThirdNews?>(url, data);
    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"> </param>
    /// <returns></returns>
    public async Task<ThirdNews?> GetDetailAsync(string id)
    {
        string url = $"/api/admin/ThirdNews/{id}";
        return await GetJsonAsync<ThirdNews?>(url);
    }

    /// <summary>
    /// ⚠删除
    /// </summary>
    /// <param name="id"> </param>
    /// <returns></returns>
    public async Task<ThirdNews?> DeleteAsync(string id)
    {
        string url = $"/api/admin/ThirdNews/{id}";
        return await DeleteJsonAsync<ThirdNews?>(url);
    }

    /// <summary>
    /// 批量操作
    /// </summary>
    /// <param name="data">ThirdNewsBatchUpdateDto</param>
    /// <returns></returns>
    public async Task<bool?> BatchUpdateAsync(ThirdNewsBatchUpdateDto data)
    {
        string url = $"/api/admin/ThirdNews/batchUpdate";
        return await PutJsonAsync<bool?>(url, data);
    }

}