using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DusiApp.Services;
public static class AppStatusService
{

    /// <summary>
    /// 判断是否登录
    /// </summary>
    /// <returns></returns>
    public static bool IsLogin()
    {
        var token = Preferences.Default.Get(Const.AccessToken, string.Empty);
        return !string.IsNullOrWhiteSpace(token);
    }
}
