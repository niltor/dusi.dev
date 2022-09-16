namespace Share.Models.EntityLibraryDtos;
/// <summary>
/// 实体库查询筛选
/// </summary>
public class EntityLibraryFilterDto : FilterBase
{
    public string? Name { get; set; }

    /// <summary>
    /// 是否公开
    /// </summary>
    public bool? IsPublic { get; set; }
    
}
