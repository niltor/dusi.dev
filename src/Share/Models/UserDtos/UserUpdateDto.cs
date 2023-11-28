using Entity;
namespace Share.Models.UserDtos;
/// <summary>
/// 用户账户更新时请求结构
/// </summary>
/// <inheritdoc cref="Entity.User"/>
public class UserUpdateDto
{
    public string? UserName { get; set; }
    /// <summary>
    /// 密码
    /// </summary>
    public string? Password { get; set; }
    [MaxLength(100)]
    public string? Email { get; set; }
    public bool? EmailConfirmed { get; set; }
    
}
