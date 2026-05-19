namespace DevTalk.API.DTOs;

// ─── Comment ──────────────────────────────────────────────────────────────────

public record CommentDto(
    int Id,
    string Body,
    int AuthorId,
    string AuthorUsername,
    int TopicId,
    DateTime CreatedAt
);

public record CreateCommentRequest(
    string Body,
    int AuthorId
);
