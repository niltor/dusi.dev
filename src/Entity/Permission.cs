
namespace Entity;
/// <summary>
/// 权限
/// </summary>
public class Permission : IEntityBase
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
    public Guid Id { get; set; }
    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset UpdatedTime { get; set; }
    public bool IsDeleted { get; set; }
}
