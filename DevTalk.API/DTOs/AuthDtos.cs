namespace DevTalk.API.DTOs;

// ─── Auth ─────────────────────────────────────────────────────────────────────

public record LoginRequest(string Email, string Password);

public record LoginResponse(
    int Id,
    string Username,
    string Email,
    string Role
);
