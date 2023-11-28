using Entity.CMS;
namespace Share.Models.ThirdNewsDtos;

/// <inheritdoc cref="Entity.CMS.ThirdNews"/>
public class ThirdNewsUpdateDto
{
    [MaxLength(200)]
    public string? Title { get; set; }
    [MaxLength(5000)]
    public string? Description { get; set; }
    [MaxLength(300)]
    public string? ThumbnailUrl { get; set; }
    [MaxLength(8000)]
    public string? Content { get; set; }
    [MaxLength(50)]
    public string? Category { get; set; }
    public NewsSource? Type { get; set; }
    public NewsType? NewsType { get; set; }
    public List<NewsTags>? NewsTags { get; set; }
    public TechType? TechType { get; set; }
    
}
