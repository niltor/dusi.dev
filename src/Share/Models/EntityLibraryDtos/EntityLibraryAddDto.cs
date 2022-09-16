namespace Share.Models.EntityLibraryDtos;
/// <summary>
/// 实体库添加时请求结构
/// </summary>
public class EntityLibraryAddDto
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
    
}
