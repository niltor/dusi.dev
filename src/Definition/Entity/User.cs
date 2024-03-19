using Entity.CMS;
using Entity.EntityDesign;

namespace Entity;

public class User : IEntityBase
{
	/// <summary>
	/// 用户名
	/// </summary>
	[MaxLength(40)]
	public required string UserName { get; set; }
	public UserType UserType { get; set; } = UserType.Normal;
	[MaxLength(100)]
	public string? Email { get; set; } = null!;
	public bool EmailConfirmed { get; set; } = false;
	[MaxLength(100)]
	public string PasswordHash { get; set; } = default!;
	[MaxLength(60)]
	public string PasswordSalt { get; set; } = default!;

	public List<EntityLibrary>? EntityLibraries { get; set; }
	public List<EntityModel>? EntityModels { get; set; }
	public List<Blog>? Blogs { get; set; }
	public List<Catalog>? Catalogs { get; set; }
	public List<Tags>? Tags { get; set; }
	public List<OpenSourceProduct>? OpenSources { get; set; }
    public Guid Id { get; set; }
    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset UpdatedTime { get; set; }
    public bool IsDeleted { get; set; }
}
public enum UserType
{
	/// <summary>
	/// 普通用户
	/// </summary>
	Normal,
	/// <summary>
	/// 认证用户
	/// </summary>
	Verify,
	/// <summary>
	/// 会员
	/// </summary>
	Member
}
