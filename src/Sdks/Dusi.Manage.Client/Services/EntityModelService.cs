using Share.Models.EntityModelDtos;
namespace Dusi.Manage.Client.Services;
/// <summary>
/// 实体模型类
/// </summary>
public class EntityModelService : BaseService
{
    public EntityModelService(HttpClient httpClient) : base(httpClient)
    {

    }
    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="data">EntityModelFilterDto</param>
    /// <returns></returns>
    public async Task<PageList<EntityModelItemDto>?> FilterAsync(EntityModelFilterDto data)
    {
        string url = $"/api/admin/EntityModel/filter";
        return await PostJsonAsync<PageList<EntityModelItemDto>?>(url, data);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="data">EntityModelAddDto</param>
    /// <returns></returns>
    public async Task<EntityModel?> AddAsync(EntityModelAddDto data)
    {
        string url = $"/api/admin/EntityModel";
        return await PostJsonAsync<EntityModel?>(url, data);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"> </param>
    /// <param name="data">EntityModelUpdateDto</param>
    /// <returns></returns>
    public async Task<EntityModel?> UpdateAsync(string id, EntityModelUpdateDto data)
    {
        string url = $"/api/admin/EntityModel/{id}";
        return await PutJsonAsync<EntityModel?>(url, data);
    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"> </param>
    /// <returns></returns>
    public async Task<EntityModel?> GetDetailAsync(string id)
    {
        string url = $"/api/admin/EntityModel/{id}";
        return await GetJsonAsync<EntityModel?>(url);
    }

    /// <summary>
    /// ⚠删除
    /// </summary>
    /// <param name="id"> </param>
    /// <returns></returns>
    public async Task<EntityModel?> DeleteAsync(string id)
    {
        string url = $"/api/admin/EntityModel/{id}";
        return await DeleteJsonAsync<EntityModel?>(url);
    }

}