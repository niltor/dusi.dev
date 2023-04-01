using Core.Entities.CMS;
namespace Share.Models.ThirdNewsDtos;

/// <inheritdoc cref="Core.Entities.CMS.ThirdNews"/>
public class ThirdNewsFilterDto : FilterBase
{
    [MaxLength(100)]
    public string? AuthorName { get; set; }
    [MaxLength(200)]
    public string? Title { get; set; }

    [MaxLength(50)]
    public string? Category { get; set; }

    public NewsSource? Type { get; set; }
    public NewsType? NewsType { get; set; }
    public TechType? TechType { get; set; }
    public NewsStatus? NewsStatus { get; set; }

    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }

    /// <summary>
    /// 是否本周
    /// </summary>
    public bool? OnlyWeek { get; set; }

    /// <summary>
    /// 是否被分类
    /// </summary>
    public bool? IsClassified { get; set; }
}
