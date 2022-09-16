
namespace Share.Models.EntityModelDtos;
/// <summary>
/// 实体模型类概要
/// </summary>
public class EntityModelShortDto
{
    /// <summary>
    /// 实体类名
    /// </summary>
    [MaxLength(60)]
    public string Name { get; set; } = default!;

    /// <summary>
    /// 实体注释内容
    /// </summary>
    [MaxLength(300)]
    public string Comment { get; set; } = default!;

    /// <summary>
    /// 访问修饰符
    /// </summary>
    public AccessModifier AccessModifier { get; set; } = default!;
    public CodeLanguage? CodeLanguage { get; set; }

    /// <summary>
    /// 父类
    /// </summary>
    public EntityModel? ParentEntity { get; set; } = default!;

    /// <summary>
    /// 所属模型库
    /// </summary>
    public EntityLibrary EntityLibrary { get; set; } = default!;
    public Guid Id { get; set; } = default!;
    public DateTimeOffset CreatedTime { get; set; } = default!;
    public DateTimeOffset UpdatedTime { get; set; } = default!;
    /// <summary>
    /// 软删除
    /// </summary>
    public bool IsDeleted { get; set; } = default!;

}
