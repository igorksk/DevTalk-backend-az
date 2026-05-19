using DevTalk.API.Data;
using DevTalk.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DevTalk.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;

    public AuthController(AppDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Simple credential-based login — no JWT, returns user info to be stored client-side.
    /// In a real project, use ASP.NET Identity + JWT or cookie auth.
    /// </summary>
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            return BadRequest(new { message = "Email and password are required." });

        var user = _db.Users.FirstOrDefault(u =>
            u.Email.ToLower() == request.Email.ToLower() &&
            u.PasswordHash == request.Password); // plain-text compare for demo

        if (user is null)
            return Unauthorized(new { message = "Invalid email or password." });

        return Ok(new LoginResponse(user.Id, user.Username, user.Email, user.Role));
    }
}
