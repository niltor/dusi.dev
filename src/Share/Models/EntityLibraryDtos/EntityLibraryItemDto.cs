namespace Share.Models.EntityLibraryDtos;
/// <summary>
/// 实体库列表元素
/// </summary>
public class EntityLibraryItemDto
{
    public string Name { get; set; } = default!;
    /// <summary>
    /// 库描述
    /// </summary>
    [MaxLength(200)]
    public string? Description { get; set; }

    /// <summary>
    /// 是否公开
    /// </summary>
    public bool IsPublic { get; set; } = default!;
    public Guid Id { get; set; } = default!;
    public DateTimeOffset CreatedTime { get; set; } = default!;
    public DateTimeOffset UpdatedTime { get; set; } = default!;
    /// <summary>
    /// 软删除
    /// </summary>
    public bool IsDeleted { get; set; } = default!;
    
}
