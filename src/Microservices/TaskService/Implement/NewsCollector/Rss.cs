namespace TaskService.Implement.NewsCollector;

public class Rss
{
    public string? Categories { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.UtcNow;
    public string? Link { get; set; }
    public string? Author { get; set; }
    public int PublishId { get; set; }
    public DateTimeOffset LastUpdateTime { get; set; }
    public string? Content { get; set; }
    public string? ThumbUrl { get; set; }
    /// <summary>
    /// ��Դ
    /// </summary>
    public string? Provider { get; set; }
}
