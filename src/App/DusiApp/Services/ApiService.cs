
using Dusi.Manage.Client;

namespace DusiApp.Services;
public static class ApiService
{
    public readonly static AdminClient AdminClient = new("http://10.0.2.2:5002/");
}
