using Ater.Web.Abstraction.Interface;

namespace Application;

/// <summary>
/// 用户上下文
/// </summary>
public interface IUserContext : IUserContextBase
{
    Claim? FindClaim(string claimType);
    Task<User?> GetUserAsync();
}
