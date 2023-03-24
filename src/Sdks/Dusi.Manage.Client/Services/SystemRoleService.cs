using Share.Models.SystemRoleDtos;
namespace Dusi.Manage.Client.Services;
/// <summary>
/// 角色表
/// </summary>
public class SystemRoleService : BaseService
{
    public SystemRoleService(HttpClient httpClient) : base(httpClient)
    {

    }
    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="data">SystemRoleFilterDto</param>
    /// <returns></returns>
    public async Task<PageList<SystemRoleItemDto>?> FilterAsync(SystemRoleFilterDto data)
    {
        string url = $"/api/admin/SystemRole/filter";
        return await PostJsonAsync<PageList<SystemRoleItemDto>?>(url, data);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="data">SystemRoleAddDto</param>
    /// <returns></returns>
    public async Task<SystemRole?> AddAsync(SystemRoleAddDto data)
    {
        string url = $"/api/admin/SystemRole";
        return await PostJsonAsync<SystemRole?>(url, data);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"> </param>
    /// <param name="data">SystemRoleUpdateDto</param>
    /// <returns></returns>
    public async Task<SystemRole?> UpdateAsync(string id, SystemRoleUpdateDto data)
    {
        string url = $"/api/admin/SystemRole/{id}";
        return await PutJsonAsync<SystemRole?>(url, data);
    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"> </param>
    /// <returns></returns>
    public async Task<SystemRole?> GetDetailAsync(string id)
    {
        string url = $"/api/admin/SystemRole/{id}";
        return await GetJsonAsync<SystemRole?>(url);
    }

    /// <summary>
    /// ⚠删除
    /// </summary>
    /// <param name="id"> </param>
    /// <returns></returns>
    public async Task<SystemRole?> DeleteAsync(string id)
    {
        string url = $"/api/admin/SystemRole/{id}";
        return await DeleteJsonAsync<SystemRole?>(url);
    }

}