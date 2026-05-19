namespace DevTalk.API.DTOs;

// ─── User ─────────────────────────────────────────────────────────────────────

public record UserDto(
    int Id,
    string Username,
    string Email,
    string Role,
    DateTime CreatedAt,
    int TopicCount,
    int CommentCount
);
