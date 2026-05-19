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

// Detail DTO — returned by GET /api/topics/{id}, includes full comment list
public record TopicDetailDto(
    int Id,
    string Title,
    string Body,
    string Category,
    int AuthorId,
    string AuthorUsername,
    DateTime CreatedAt,
    int ViewCount,
    int CommentCount,
    IEnumerable<CommentDto> Comments
);

// Request DTO — used when creating a new topic
public record CreateTopicRequest(
    string Title,
    string Body,
    string Category,
    int AuthorId
);
