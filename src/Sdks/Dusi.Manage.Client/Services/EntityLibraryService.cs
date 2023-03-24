using Share.Models.EntityLibraryDtos;
namespace Dusi.Manage.Client.Services;
/// <summary>
/// 实体库
/// </summary>
public class EntityLibraryService : BaseService
{
    public EntityLibraryService(HttpClient httpClient) : base(httpClient)
    {

    }
    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="data">EntityLibraryFilterDto</param>
    /// <returns></returns>
    public async Task<PageList<EntityLibraryItemDto>?> FilterAsync(EntityLibraryFilterDto data)
    {
        string url = $"/api/admin/EntityLibrary/filter";
        return await PostJsonAsync<PageList<EntityLibraryItemDto>?>(url, data);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="data">EntityLibraryAddDto</param>
    /// <returns></returns>
    public async Task<EntityLibrary?> AddAsync(EntityLibraryAddDto data)
    {
        string url = $"/api/admin/EntityLibrary";
        return await PostJsonAsync<EntityLibrary?>(url, data);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"> </param>
    /// <param name="data">EntityLibraryUpdateDto</param>
    /// <returns></returns>
    public async Task<EntityLibrary?> UpdateAsync(string id, EntityLibraryUpdateDto data)
    {
        string url = $"/api/admin/EntityLibrary/{id}";
        return await PutJsonAsync<EntityLibrary?>(url, data);
    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"> </param>
    /// <returns></returns>
    public async Task<EntityLibrary?> GetDetailAsync(string id)
    {
        string url = $"/api/admin/EntityLibrary/{id}";
        return await GetJsonAsync<EntityLibrary?>(url);
    }

    /// <summary>
    /// ⚠删除
    /// </summary>
    /// <param name="id"> </param>
    /// <returns></returns>
    public async Task<EntityLibrary?> DeleteAsync(string id)
    {
        string url = $"/api/admin/EntityLibrary/{id}";
        return await DeleteJsonAsync<EntityLibrary?>(url);
    }

}