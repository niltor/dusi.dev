using Entity.CMS;
namespace Share.Models.ThirdNewsDtos;

/// <inheritdoc cref="Entity.CMS.ThirdNews"/>
public class ThirdNewsAddDto
{
    [MaxLength(200)]
    public required string Title { get; set; }
    [MaxLength(5000)]
    public string? Description { get; set; }
    [MaxLength(8000)]
    public string? Content { get; set; }
    public NewsSource Type { get; set; } = NewsSource.News;
    public NewsType NewsType { get; set; } = NewsType.Default;
    public List<NewsTags>? NewsTags { get; set; }
    public TechType TechType { get; set; } = TechType.Default;

}
