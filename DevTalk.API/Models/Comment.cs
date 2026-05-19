namespace DevTalk.API.Models;

public class Comment
{
    public int Id { get; set; }

    public string Body { get; set; } = string.Empty;

    public int AuthorId { get; set; }

    public int TopicId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public User Author { get; set; } = null!;
    public ForumTopic Topic { get; set; } = null!;
}
