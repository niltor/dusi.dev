
namespace Entity.EntityDesign;
/// <summary>
/// 实体库
/// </summary>
public class EntityLibrary : IEntityBase
{
	/// <summary>
	/// 库名称
	/// </summary>
	[MaxLength(60)]
	public required string Name { get; set; }
	/// <summary>
	/// 库描述
	/// </summary>
	[MaxLength(200)]
	public string? Description { get; set; }

	/// <summary>
	/// 是否公开
	/// </summary>
	public bool IsPublic { get; set; } = true;

	/// <summary>
	/// 包含的实体类
	/// </summary>
	public List<EntityModel>? EntityModels { get; set; }

	/// <summary>
	/// 所属用户
	/// </summary>
	public required User User { get; set; }
    public Guid Id { get; set; }
    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset UpdatedTime { get; set; }
    public bool IsDeleted { get; set; }
}
