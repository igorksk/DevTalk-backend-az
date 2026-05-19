namespace DevTalk.API.Models;

public class NewsArticle
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Summary { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;

    // Categories: "Career", ".NET", "Frontend", "Gamedev", "Ukraine IT"
    public string Category { get; set; } = string.Empty;

    public string AuthorName { get; set; } = string.Empty;

    public DateTime PublishedAt { get; set; } = DateTime.UtcNow;

    public int ViewCount { get; set; } = 0;
}
