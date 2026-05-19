namespace DevTalk.API.Models;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    // Stored as plain text for simplicity in this pet project
    // In production, use BCrypt or ASP.NET Identity password hashing
    public string PasswordHash { get; set; } = string.Empty;

    public string Role { get; set; } = "User"; // "Admin", "User", "Recruiter"

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public ICollection<ForumTopic> Topics { get; set; } = new List<ForumTopic>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
