namespace DevTalk.API.Models;

public class ForumTopic
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty; // e.g. "General", ".NET", "Frontend", "Career"

    public int AuthorId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int ViewCount { get; set; } = 0;

    // Navigation properties
    public User Author { get; set; } = null!;
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
