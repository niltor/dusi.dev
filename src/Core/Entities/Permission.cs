namespace Core.Entities;
/// <summary>
/// 权限
/// </summary>
public class Permission : EntityBase
{
    [MaxLength(30)]
    public required string Name { get; set; }
    /// <summary>
    /// 父级权限
    /// </summary>
    public Permission? Parent { get; set; }
    /// <summary>
    /// 权限路径
    /// </summary>
    [MaxLength(200)]
    public string? PermissionPath { get; set; }
    public List<SystemRole>? Roles { get; set; }
    public List<RolePermission>? RolePermissions { get; set; }
}
