using DevTalk.API.Data;
using DevTalk.API.DTOs;
using DevTalk.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevTalk.API.Controllers;

[ApiController]
[Route("api/topics")]
public class TopicsController : ControllerBase
{
    private readonly AppDbContext _db;

    public TopicsController(AppDbContext db)
    {
        _db = db;
    }

    // GET /api/topics — returns all topics sorted by newest first
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var topics = await _db.ForumTopics
            .Include(t => t.Author)
            .Include(t => t.Comments)
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => MapToDto(t))
            .ToListAsync();

        return Ok(topics);
    }

    // GET /api/topics/{id} — returns a single topic with all comments
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var topic = await _db.ForumTopics
            .Include(t => t.Author)
            .Include(t => t.Comments)
                .ThenInclude(c => c.Author)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (topic is null) return NotFound(new { message = "Topic not found." });

        // Increment view count on each detail page visit
        topic.ViewCount++;
        await _db.SaveChangesAsync();

        var comments = topic.Comments
            .OrderBy(c => c.CreatedAt)
            .Select(c => new CommentDto(c.Id, c.Body, c.AuthorId, c.Author?.Username ?? "Unknown", c.TopicId, c.CreatedAt));

        return Ok(new TopicDetailDto(
            topic.Id, topic.Title, topic.Body, topic.Category,
            topic.AuthorId, topic.Author?.Username ?? "Unknown",
            topic.CreatedAt, topic.ViewCount, topic.Comments.Count, comments));
    }

    // POST /api/topics — creates a new forum topic
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTopicRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.Body))
            return BadRequest(new { message = "Title and body are required." });

        if (string.IsNullOrWhiteSpace(request.Category))
            return BadRequest(new { message = "Category is required." });

        var authorExists = await _db.Users.AnyAsync(u => u.Id == request.AuthorId);
        if (!authorExists) return BadRequest(new { message = "Author not found." });

        var topic = new ForumTopic
        {
            Title = request.Title.Trim(),
            Body = request.Body.Trim(),
            Category = request.Category.Trim(),
            AuthorId = request.AuthorId,
            CreatedAt = DateTime.UtcNow
        };

        _db.ForumTopics.Add(topic);
        await _db.SaveChangesAsync();

        // Reload with navigation properties for the response
        await _db.Entry(topic).Reference(t => t.Author).LoadAsync();

        return CreatedAtAction(nameof(GetById), new { id = topic.Id }, MapToDto(topic));
    }

    // POST /api/topics/{id}/comments — adds a comment to a topic
    [HttpPost("{id:int}/comments")]
    public async Task<IActionResult> AddComment(int id, [FromBody] CreateCommentRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Body))
            return BadRequest(new { message = "Comment body is required." });

        var topic = await _db.ForumTopics.FindAsync(id);
        if (topic is null) return NotFound(new { message = "Topic not found." });

        var author = await _db.Users.FindAsync(request.AuthorId);
        if (author is null) return BadRequest(new { message = "Author not found." });

        var comment = new Comment
        {
            Body = request.Body.Trim(),
            AuthorId = request.AuthorId,
            TopicId = id,
            CreatedAt = DateTime.UtcNow
        };

        _db.Comments.Add(comment);
        await _db.SaveChangesAsync();

        return Ok(new CommentDto(
            comment.Id,
            comment.Body,
            comment.AuthorId,
            author.Username,
            comment.TopicId,
            comment.CreatedAt
        ));
    }

    // Helper — maps a ForumTopic entity to a TopicDto
    private static TopicDto MapToDto(ForumTopic t) => new(
        t.Id,
        t.Title,
        t.Body,
        t.Category,
        t.AuthorId,
        t.Author?.Username ?? "Unknown",
        t.CreatedAt,
        t.ViewCount,
        t.Comments?.Count ?? 0
    );
}
