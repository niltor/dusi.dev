using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dusi.Manage.Client;
public interface IManagerClient
{
    public string? AccessToken { get; set; }
    public ErrorResult? ErrorMsg { get; set; }
    Task<bool> RefreshTokenAsync(string key, string secret);
    void SetBaseUrl(string url);

}
