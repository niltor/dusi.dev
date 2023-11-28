
namespace Entity;

public class SystemRole : IEntityBase
{
	/// <summary>
	/// 角色显示名称
	/// </summary>
	[MaxLength(30)]
	public required string Name { get; set; }
	/// <summary>
	/// 角色名，系统标识
	/// </summary>
	public string NameValue { get; set; } = string.Empty;
	/// <summary>
	/// 是否系统内置,系统内置不可删除
	/// </summary>
	public bool IsSystem { get; set; } = false;
	/// <summary>
	/// 图标
	/// </summary>
	[MaxLength(30)]
	public string? Icon { get; set; }

	public ICollection<SystemUser>? Users { get; set; }
    public Guid Id { get; set; }
    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset UpdatedTime { get; set; }
    public bool IsDeleted { get; set; }
}

