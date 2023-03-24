using Share.Models.AuthDtos;
namespace Dusi.Manage.Client.Services;
/// <summary>
/// 系统用户授权登录
/// </summary>
public class AuthService : BaseService
{
    public AuthService(HttpClient httpClient) : base(httpClient)
    {

    }
    /// <summary>
    /// 登录获取Token
    /// </summary>
    /// <param name="data">LoginDto</param>
    /// <returns></returns>
    public async Task<AuthResult?> LoginAsync(LoginDto data)
    {
        string url = $"/api/admin/Auth";
        return await PostJsonAsync<AuthResult?>(url, data);
    }

    /// <summary>
    /// 退出
    /// </summary>
    /// <param name="id">string </param>
    /// <returns></returns>
    public async Task<bool?> LogoutAsync(string id)
    {
        string url = $"/api/admin/Auth/{id}";
        return await GetJsonAsync<bool?>(url);
    }

}