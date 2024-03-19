using Microsoft.EntityFrameworkCore;

namespace Entity.CMS;
/// <summary>
/// 开源作品
/// </summary>
[Index(nameof(Title))]
public sealed class OpenSourceProduct : IEntityBase
{
	/// <summary>
	/// 标题
	/// </summary>
	[MaxLength(100)]
	public required string Title { get; set; }
	/// <summary>
	/// project url address
	/// </summary>
	[MaxLength(200)]
	public required string ProjectUrl { get; set; }
	/// <summary>
	/// 描述
	/// </summary>
	[MaxLength(500)]
	public required string Description { get; set; }

	/// <summary>
	/// 缩略图
	/// </summary>
	[MaxLength(200)]
	public string? Thumbnail { get; set; }
	/// <summary>
	/// 作者
	/// </summary>
	[MaxLength(60)]
	public string? Authors { get; set; }
	/// <summary>
	/// 标签
	/// </summary>
	[MaxLength(300)]
	public string? Tags { get; set; }

	/// <summary>
	/// 所属用户
	/// </summary>
	public User? User { get; set; }
    public Guid Id { get; set; }
    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset UpdatedTime { get; set; }
    public bool IsDeleted { get; set; }
}


