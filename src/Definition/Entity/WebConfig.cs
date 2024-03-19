using Ater.Web.Core.Models;

namespace Entity;
/// <summary>
/// 网站配置
/// </summary>
public class WebConfig : IEntityBase
{
	public WebConfig(string key, string value = "")
	{
		Key = key;
		Value = value;
	}
	public WebConfig()
	{
	}

	[MaxLength(100)]
	public string Key { get; init; } = default!;
	[MaxLength(100)]
	public string Value { get; set; } = string.Empty;
	[MaxLength(300)]
	public string? Description { get; set; }
	public bool Valid { get; set; } = true;

	/// <summary>
	/// 是否属于系统配置
	/// </summary>
	public bool IsSystem { get; set; }

	/// <summary>
	/// 组
	/// </summary>
	public string? GroupName { get; set; }
    public Guid Id { get; set; }
    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset UpdatedTime { get; set; }
    public bool IsDeleted { get; set; }
}
