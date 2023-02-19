namespace Share.Models.UserDtos;
/// <summary>
/// 系统用户添加时请求结构
/// </summary>
public class UserAddDto
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; } = default!;
    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; } = default!;
    [MaxLength(100)]
    public string? Email { get; set; }
}
