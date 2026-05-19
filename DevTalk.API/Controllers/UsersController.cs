using DevTalk.API.Data;
using DevTalk.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevTalk.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _db;

    public UsersController(AppDbContext db)
    {
        _db = db;
    }

    // GET /api/users/{id} — returns public profile info for a user
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _db.Users
            .Include(u => u.Topics)
            .Include(u => u.Comments)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user is null) return NotFound(new { message = "User not found." });

        return Ok(new UserDto(
            user.Id,
            user.Username,
            user.Email,
            user.Role,
            user.CreatedAt,
            user.Topics.Count,
            user.Comments.Count
        ));
    }
}
