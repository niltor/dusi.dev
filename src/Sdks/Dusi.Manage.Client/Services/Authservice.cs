using Share.Models.AuthDtos;

namespace Dusi.Manage.Client.Services;
public class AuthService : BaseService
{
    public AuthService(HttpClient httpClient) : base(httpClient)
    {
    }


    public async Task<AuthResult?> LoginSyncAsync(LoginDto dto)
    {
        return await PostJsonAsync<AuthResult>("", dto);
    }
}
