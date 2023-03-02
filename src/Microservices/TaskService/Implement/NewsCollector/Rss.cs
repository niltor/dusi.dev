namespace TaskService.Implement.NewsCollector;

public class Rss
{
    public string? Categories { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime CreateTime { get; set; } = DateTime.UtcNow;
    public string? Link { get; set; }
    public string? Author { get; set; }
    public int PublishId { get; set; }
    public DateTime LastUpdateTime { get; set; }
    public string? Content { get; set; }
    public string? ThumbUrl { get; set; }
}
