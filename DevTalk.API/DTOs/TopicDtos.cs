namespace DevTalk.API.DTOs;

// ─── Topic ────────────────────────────────────────────────────────────────────

// Response DTO — used in list views and detail views
public record TopicDto(
    int Id,
    string Title,
    string Body,
    string Category,
    int AuthorId,
    string AuthorUsername,
    DateTime CreatedAt,
    int ViewCount,
    int CommentCount
);

// Request DTO — used when creating a new topic
public record CreateTopicRequest(
    string Title,
    string Body,
    string Category,
    int AuthorId
);
