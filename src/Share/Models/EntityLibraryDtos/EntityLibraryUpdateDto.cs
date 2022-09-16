namespace Share.Models.EntityLibraryDtos;
/// <summary>
/// 实体库更新时请求结构
/// </summary>
public class EntityLibraryUpdateDto
{
    public string? Name { get; set; }
    /// <summary>
    /// 库描述
    /// </summary>
    [MaxLength(200)]
    public string? Description { get; set; }

    /// <summary>
    /// 是否公开
    /// </summary>
    public bool? IsPublic { get; set; }
    
}
