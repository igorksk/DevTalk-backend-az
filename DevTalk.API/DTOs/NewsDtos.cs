namespace DevTalk.API.DTOs;

// ─── News ─────────────────────────────────────────────────────────────────────

public record NewsArticleDto(
    int Id,
    string Title,
    string Summary,
    string Body,
    string Category,
    string AuthorName,
    DateTime PublishedAt,
    int ViewCount
);
