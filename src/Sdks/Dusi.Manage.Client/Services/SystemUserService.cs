using Share.Models.SystemUserDtos;
namespace Dusi.Manage.Client.Services;
/// <summary>
/// 系统用户
/// </summary>
public class SystemUserService : BaseService
{
    public SystemUserService(HttpClient httpClient) : base(httpClient)
    {

    }
    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="data">SystemUserFilterDto</param>
    /// <returns></returns>
    public async Task<PageList<SystemUserItemDto>?> FilterAsync(SystemUserFilterDto data)
    {
        string url = $"/api/admin/SystemUser/filter";
        return await PostJsonAsync<PageList<SystemUserItemDto>?>(url, data);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="data">SystemUserAddDto</param>
    /// <returns></returns>
    public async Task<SystemUser?> AddAsync(SystemUserAddDto data)
    {
        string url = $"/api/admin/SystemUser";
        return await PostJsonAsync<SystemUser?>(url, data);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"> </param>
    /// <param name="data">SystemUserUpdateDto</param>
    /// <returns></returns>
    public async Task<SystemUser?> UpdateAsync(string id, SystemUserUpdateDto data)
    {
        string url = $"/api/admin/SystemUser/{id}";
        return await PutJsonAsync<SystemUser?>(url, data);
    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"> </param>
    /// <returns></returns>
    public async Task<SystemUser?> GetDetailAsync(string id)
    {
        string url = $"/api/admin/SystemUser/{id}";
        return await GetJsonAsync<SystemUser?>(url);
    }

    /// <summary>
    /// ⚠删除
    /// </summary>
    /// <param name="id"> </param>
    /// <returns></returns>
    public async Task<SystemUser?> DeleteAsync(string id)
    {
        string url = $"/api/admin/SystemUser/{id}";
        return await DeleteJsonAsync<SystemUser?>(url);
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="password"> </param>
    /// <returns></returns>
    public async Task<bool?> ChangeMyPasswordAsync(string? password)
    {
        string url = $"/api/admin/SystemUser/password?password={password}";
        return await PutJsonAsync<bool?>(url);
    }

}