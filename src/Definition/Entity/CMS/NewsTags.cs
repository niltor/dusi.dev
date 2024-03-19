
namespace Entity.CMS;
/// <summary>
/// 新闻标签
/// </summary>
public class NewsTags : IEntityBase
{
	[MaxLength(40)]
	public string Name { get; set; } = string.Empty;
	[MaxLength(20)]
	public string? Color { get; set; }
	public ThirdNews ThirdNews { get; set; } = null!;
    public Guid Id { get; set; }
    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset UpdatedTime { get; set; }
    public bool IsDeleted { get; set; }
}
