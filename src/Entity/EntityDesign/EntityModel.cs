
namespace Entity.EntityDesign;
/// <summary>
/// 实体模型类
/// </summary>
public class EntityModel : IEntityBase
{
	/// <summary>
	/// 实体类名
	/// </summary>
	[MaxLength(60)]
	public required string Name { get; set; }

	/// <summary>
	/// 实体注释内容
	/// </summary>
	[MaxLength(300)]
	public required string Comment { get; set; }

	/// <summary>
	/// 访问修饰符
	/// </summary>
	public AccessModifier AccessModifier { get; set; } = AccessModifier.Public;

	/// <summary>
	/// 代码内容
	/// </summary>
	[MaxLength(8000)]
	public string? CodeContent { get; set; }

	/// <summary>
	/// 代码语言
	/// </summary>
	public CodeLanguage CodeLanguage { get; set; } = CodeLanguage.Csharp;

	/// <summary>
	/// 语言版本
	/// </summary>
	[MaxLength(20)]
	public string LanguageVersion { get; set; } = "latest";

	/// <summary>
	/// 父类
	/// </summary>
	public EntityModel? ParentEntity { get; set; }

	/// <summary>
	/// 直属子类
	/// </summary>
	public List<EntityModel>? ChildrenEntities { get; set; }

	/// <summary>
	/// 包含的属性
	/// </summary>
	public List<EntityMember>? EntityMembers { get; set; }

	/// <summary>
	/// 所属模型库
	/// </summary>
	public required EntityLibrary EntityLibrary { get; set; }

	public required User User { get; set; }
    public Guid Id { get; set; }
    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset UpdatedTime { get; set; }
    public bool IsDeleted { get; set; }
}

/// <summary>
/// 访问修饰符
/// </summary>
public enum AccessModifier
{
	Public,
	Protected,
	Internal
}

/// <summary>
/// 编程语言
/// </summary>
public enum CodeLanguage
{
	Csharp,
	Fsharp,
	Java,
	Php,
	Python,
	Kotlin,
	Swift,
	Typescript,
	Javascript,
	Html,
	Css,
	Dart,
	Rust,
	Cpp,
	Golang,
	Node,
	Deno,
	Markdown,
	Text,
	Shell,
	Powershell,
	Json,
	Xml,
	Else
}