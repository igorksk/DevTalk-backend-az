# DevTalk UA — Backend

ASP.NET Core 8 Web API with EF Core In-Memory database.

## Tech Stack

- **Framework:** ASP.NET Core 8
- **ORM:** Entity Framework Core 8 (In-Memory provider)
- **Docs:** Swagger / Swashbuckle
- **Language:** C# 12

## Project Structure

```
DevTalk.API/
├── Controllers/
│   ├── AuthController.cs       # POST /api/auth/login
│   ├── TopicsController.cs     # Forum topic CRUD + comments
│   ├── NewsController.cs       # News articles
│   └── UsersController.cs      # User profiles
├── Data/
│   ├── AppDbContext.cs         # EF Core DbContext
│   └── SeedData.cs             # Demo data seeded on startup
├── DTOs/
│   ├── AuthDtos.cs
│   ├── TopicDtos.cs
│   ├── CommentDtos.cs
│   ├── NewsDtos.cs
│   └── UserDtos.cs
├── Models/
│   ├── User.cs
│   ├── ForumTopic.cs
│   ├── Comment.cs
│   └── NewsArticle.cs
├── appsettings.json
└── Program.cs
```

## Getting Started

```bash
cd backend/DevTalk.API
dotnet restore
dotnet run
```

- API base URL: `http://localhost:5000`
- Swagger UI: `http://localhost:5000/swagger`

> The in-memory database is seeded fresh on every startup.

## API Endpoints

| Method | Path | Description |
|--------|------|-------------|
| `POST` | `/api/auth/login` | Login with email + password |
| `GET` | `/api/topics` | List all forum topics |
| `GET` | `/api/topics/{id}` | Topic detail (with comments) |
| `POST` | `/api/topics` | Create a new topic |
| `POST` | `/api/topics/{id}/comments` | Add a comment to a topic |
| `GET` | `/api/news` | List news (optional `?category=`) |
| `GET` | `/api/news/{id}` | News article detail |
| `GET` | `/api/users/{id}` | User profile |

## Seed Accounts

| Email | Password | Role |
|-------|----------|------|
| admin@devtalk.ua | Admin123! | Admin |
| user@devtalk.ua | User123! | User |
| recruiter@devtalk.ua | Recruiter123! | Recruiter |

## Notes

- Passwords are stored as plain text intentionally for this demo. Use BCrypt / ASP.NET Identity in production.
- CORS is configured to allow `http://localhost:5173` (Vite dev server).
- No JWT — the login endpoint returns the user object directly.
