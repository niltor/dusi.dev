using Microsoft.AspNetCore.Http;

namespace Application.Implement;

public class UserContext : IUserContext
{
    public Guid UserId { get; init; }
    public Guid? SessionId { get; init; }
    public string? Username { get; init; }
    public string? Email { get; set; }
    public bool IsAdmin { get; init; } = false;
    public string? CurrentRole { get; set; }
    public List<string>? Roles { get; set; }
    public Guid? GroupId { get; init; }
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly CommandDbContext _context;
    public UserContext(IHttpContextAccessor httpContextAccessor, CommandDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        if (Guid.TryParse(FindClaim(ClaimTypes.NameIdentifier)?.Value, out Guid userId) && userId != Guid.Empty)
        {
            UserId = userId;
        }
        if (Guid.TryParse(FindClaim(ClaimTypes.GroupSid)?.Value, out Guid groupSid) && groupSid != Guid.Empty)
        {
            GroupId = groupSid;
        }
        Username = FindClaim(ClaimTypes.Name)?.Value;
        Email = FindClaim(ClaimTypes.Email)?.Value;

        CurrentRole = FindClaim(ClaimTypes.Role)?.Value;

        Roles = _httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role)
            .Select(c => c.Value).ToList();
        if (Roles != null)
        {
            IsAdmin = Roles.Any(r => r.ToLower().Equals("admin"));
        }
        _context = context;
    }

    public Claim? FindClaim(string claimType)
    {
        return _httpContextAccessor?.HttpContext?.User?.FindFirst(claimType);
    }

    /// <summary>
    /// �жϵ�ǰ��ɫ
    /// </summary>
    /// <param name="roleName"></param>
    /// <returns></returns>
    public bool IsRole(string roleName)
    {
        return Roles != null && Roles.Any(r => r.Equals(roleName));
    }

    /// <summary>
    /// 是否存在
    /// </summary>
    /// <returns></returns>
    public async Task<bool> ExistAsync()
    {
        return IsAdmin ?
            await _context.SystemUsers.AnyAsync(u => u.Id == UserId) :
            await _context.Users.AnyAsync(u => u.Id == UserId);
    }

    /// <summary>
    /// 获取ip地址
    /// </summary>
    /// <returns></returns>
    public string? GetIpAddress()
    {
        HttpRequest? request = _httpContextAccessor.HttpContext?.Request;
        return request == null
            ? string.Empty
            : request.Headers.ContainsKey("X-Forwarded-For")
            ? request.Headers["X-Forwarded-For"].Where(x => x != null).FirstOrDefault()
            : _httpContextAccessor.HttpContext!.Connection.RemoteIpAddress?.ToString();
    }

    public async Task<User?> GetUserAsync()
    {
        return await _context.Users.FindAsync(UserId);
    }
    public async Task<TUser?> GetUserAsync<TUser>() where TUser : class
    {
        return await _context.Set<TUser>().FindAsync(UserId);
    }
}
