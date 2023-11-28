using Entity.CMS;
namespace Share.Models.ThirdNewsDtos;

/// <inheritdoc cref="Entity.CMS.ThirdNews"/>
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
    /// �Ƿ���
    /// </summary>
    public bool? OnlyWeek { get; set; }

    /// <summary>
    /// �Ƿ񱻷���
    /// </summary>
    public bool? IsClassified { get; set; }
}
