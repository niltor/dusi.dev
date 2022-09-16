using Core.Entities.EntityDesign;

namespace Share.Models.EntityMemberDtos;
/// <summary>
/// 实体属性添加时请求结构
/// </summary>
public class EntityMemberAddDto
{
    /// <summary>
    /// 属性名
    /// </summary>
    [MaxLength(60)]
    public string Name { get; set; } = default!;
    /// <summary>
    /// 属性注释内容
    /// </summary>
    [MaxLength(300)]
    public string Comment { get; set; } = default!;
    /// <summary>
    /// 默认值，根据类型推断
    /// </summary>
    [MaxLength(100)]
    public string? DefaultValue { get; set; }

    /// <summary>
    /// 访问修饰符
    /// </summary>
    public AccessModifier AccessModifier { get; set; } = default!;

    /// <summary>
    /// 是否必须
    /// </summary>
    public bool IsRequired { get; set; } = default!;
    /// <summary>
    /// 是否为枚举
    /// </summary>
    public bool IsEnum { get; set; } = default!;
    /// <summary>
    /// 是否为列表
    /// </summary>
    public bool IsList { get; set; } = default!;
    /// <summary>
    /// 是否为自定义对象
    /// </summary>
    public bool IsObject { get; set; } = default!;

    /// <summary>
    /// 是否可赋值
    /// </summary>
    public bool CanSet { get; set; } = default!;

    /// <summary>
    /// 是否要初始化
    /// </summary>
    public bool NeedInit { get; set; } = default!;

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
    public MemberType MemberType { get; set; } = default!;
    public Guid? ObjectTypeId { get; set; }
    public Guid ConstraintId { get; set; } = default!;
    public Guid EntityModelId { get; set; } = default!;
    
}
