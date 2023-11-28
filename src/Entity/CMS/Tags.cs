using Microsoft.EntityFrameworkCore;

namespace Entity.CMS;
/// <summary>
/// 标签
/// </summary>
[Index(nameof(Name))]
[Index(nameof(Color))]
public class Tags : IEntityBase
{
	/// <summary>
	/// 标签名称
	/// </summary>
	[MaxLength(50)]
	public required string Name { get; set; }

	/// <summary>
	/// 标签颜色
	/// </summary>
	[MaxLength(20)]
	public string? Color { get; set; }
	/// <summary>
	/// 所属用户
	/// </summary>
	public required User User { get; set; }
	/// <summary>
	/// 所属博客
	/// </summary>
	public List<Blog>? Blogs { get; set; }
    public Guid Id { get; set; }
    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset UpdatedTime { get; set; }
    public bool IsDeleted { get; set; }
}
