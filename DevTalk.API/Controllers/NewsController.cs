using DevTalk.API.Data;
using DevTalk.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevTalk.API.Controllers;

[ApiController]
[Route("api/news")]
public class NewsController : ControllerBase
{
    private readonly AppDbContext _db;

    public NewsController(AppDbContext db)
    {
        _db = db;
    }

    // GET /api/news — returns all articles, newest first
    // Optional query param: ?category=.NET
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? category)
    {
        var query = _db.NewsArticles.AsQueryable();

        if (!string.IsNullOrWhiteSpace(category))
            query = query.Where(a => a.Category == category);

        var articles = await query
            .OrderByDescending(a => a.PublishedAt)
            .Select(a => new NewsArticleDto(
                a.Id, a.Title, a.Summary, a.Body,
                a.Category, a.AuthorName, a.PublishedAt, a.ViewCount))
            .ToListAsync();

        return Ok(articles);
    }

    // GET /api/news/{id} — returns a single article
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var article = await _db.NewsArticles.FindAsync(id);
        if (article is null) return NotFound(new { message = "Article not found." });

        // Increment view count
        article.ViewCount++;
        await _db.SaveChangesAsync();

        return Ok(new NewsArticleDto(
            article.Id, article.Title, article.Summary, article.Body,
            article.Category, article.AuthorName, article.PublishedAt, article.ViewCount));
    }
}
