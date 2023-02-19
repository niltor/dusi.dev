namespace Share.Models.UserDtos;
/// <summary>
/// 系统用户更新时请求结构
/// </summary>
public class UserUpdateDto
{
    public string? UserName { get; set; }
    /// <summary>
    /// 真实姓名
    /// </summary>
    [MaxLength(30)]
    public string? RealName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string? Password { get; set; }
    [MaxLength(100)]
    public string? Email { get; set; }
    public bool? EmailConfirmed { get; set; }
    // public string? PasswordHash { get; set; }
    // public string? PasswordSalt { get; set; }
}
