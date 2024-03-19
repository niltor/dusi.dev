namespace Entity.EntityDesign;
/// <summary>
/// 实体属性
/// </summary>
public class EntityMember : IEntityBase
{
	/// <summary>
	/// 属性名
	/// </summary>
	[MaxLength(60)]
	public required string Name { get; set; }
	/// <summary>
	/// 属性注释内容
	/// </summary>
	[MaxLength(300)]
	public required string Comment { get; set; }
	/// <summary>
	/// 默认值，根据类型推断
	/// </summary>
	[MaxLength(100)]
	public string? DefaultValue { get; set; }

	/// <summary>
	/// 访问修饰符
	/// </summary>
	public required AccessModifier AccessModifier { get; set; } = AccessModifier.Public;

	/// <summary>
	/// 是否必须
	/// </summary>
	public bool IsRequired { get; set; } = false;
	/// <summary>
	/// 是否为枚举
	/// </summary>
	public bool IsEnum { get; set; } = false;
	/// <summary>
	/// 是否为列表
	/// </summary>
	public bool IsList { get; set; } = false;
	/// <summary>
	/// 是否为自定义对象
	/// </summary>
	public bool IsObject { get; set; } = false;

	/// <summary>
	/// 是否可赋值
	/// </summary>
	public bool CanSet { get; set; } = true;

	/// <summary>
	/// 是否要初始化
	/// </summary>
	public bool NeedInit { get; set; } = false;

	/// <summary>
	/// 字典的键类型
	/// </summary>
	public MemberType? DictionaryKeyType { get; set; }

	/// <summary>
	/// 字典的值类型
	/// </summary>
	public MemberType? DictionaryValueType { get; set; }

	/// <summary>
	/// 属性类型
	/// </summary>
	public required MemberType MemberType { get; set; } = MemberType.String;

	/// <summary>
	/// 约束
	/// </summary>
	public EntityMemberConstraint? Constraint { get; set; }

	/// <summary>
	/// 对象类型
	/// </summary>
	public EntityModel? ObjectType { get; set; }
	public Guid? ObjectTypeId { get; set; }

	/// <summary>
	/// 所属模型
	/// </summary>
	public required EntityModel EntityModel { get; set; }
    public Guid Id { get; set; }
    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset UpdatedTime { get; set; }
    public bool IsDeleted { get; set; }
}
/// <summary>
/// 属性的类型
/// </summary>
public enum MemberType
{
	Byte,
	Short,
	Int,
	Long,
	Double,
	Float,
	Decimal,
	String,
	Array,
	List,
	Dictionary,
	Customer
}



