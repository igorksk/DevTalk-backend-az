using DevTalk.API.Models;

namespace DevTalk.API.Data;

// Seeds the in-memory database with initial data on startup
public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        // Only seed if database is empty
        if (context.Users.Any()) return;

        // ─── USERS ────────────────────────────────────────────────────
        // Login: admin@devtalk.ua    | Password: Admin123!
        // Login: user@devtalk.ua     | Password: User123!
        // Login: recruiter@devtalk.ua| Password: Recruiter123!
        var users = new List<User>
        {
            new User
            {
                Id = 1,
                Username = "Admin",
                Email = "admin@devtalk.ua",
                PasswordHash = "Admin123!",   // plain text for demo; use BCrypt in production
                Role = "Admin",
                CreatedAt = new DateTime(2025, 1, 10, 9, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                Id = 2,
                Username = "OleksandrK",
                Email = "user@devtalk.ua",
                PasswordHash = "User123!",
                Role = "User",
                CreatedAt = new DateTime(2025, 2, 5, 10, 30, 0, DateTimeKind.Utc)
            },
            new User
            {
                Id = 3,
                Username = "IvanRecruiter",
                Email = "recruiter@devtalk.ua",
                PasswordHash = "Recruiter123!",
                Role = "Recruiter",
                CreatedAt = new DateTime(2025, 3, 12, 14, 0, 0, DateTimeKind.Utc)
            }
        };
        context.Users.AddRange(users);

        // ─── FORUM TOPICS ─────────────────────────────────────────────
        var topics = new List<ForumTopic>
        {
            new ForumTopic
            {
                Id = 1,
                Title = "Is .NET still a good choice for backend in 2026?",
                Body = "I've been using .NET since version 4.5 and wondering if it still makes sense to invest time learning ASP.NET Core in 2026. What do you think?",
                Category = ".NET",
                AuthorId = 2,
                CreatedAt = new DateTime(2025, 4, 1, 11, 0, 0, DateTimeKind.Utc),
                ViewCount = 312
            },
            new ForumTopic
            {
                Id = 2,
                Title = "React vs Angular for enterprise projects",
                Body = "Our team is evaluating frontend frameworks for a large-scale enterprise application. React seems more flexible but Angular has a stronger structure. Thoughts?",
                Category = "Frontend",
                AuthorId = 1,
                CreatedAt = new DateTime(2025, 4, 10, 9, 15, 0, DateTimeKind.Utc),
                ViewCount = 205
            },
            new ForumTopic
            {
                Id = 3,
                Title = "How to find part-time .NET work in Ukraine?",
                Body = "Looking for part-time remote projects as a .NET developer based in Kyiv. Any platforms or communities you'd recommend besides Upwork?",
                Category = "Career",
                AuthorId = 3,
                CreatedAt = new DateTime(2025, 4, 18, 16, 45, 0, DateTimeKind.Utc),
                ViewCount = 178
            },
            new ForumTopic
            {
                Id = 4,
                Title = "Steam indie game development as a side project",
                Body = "Anyone here building indie games in their free time and releasing on Steam? I'm using Unity + C# and curious about the business model side.",
                Category = "Gamedev",
                AuthorId = 2,
                CreatedAt = new DateTime(2025, 5, 2, 12, 0, 0, DateTimeKind.Utc),
                ViewCount = 143
            },
            new ForumTopic
            {
                Id = 5,
                Title = "Best way to structure ASP.NET Core Web API?",
                Body = "Clean Architecture, Minimal APIs, Controllers with services, or something else? Would love to see how experienced devs structure their ASP.NET Core projects.",
                Category = ".NET",
                AuthorId = 1,
                CreatedAt = new DateTime(2025, 5, 14, 8, 30, 0, DateTimeKind.Utc),
                ViewCount = 267
            }
        };
        context.ForumTopics.AddRange(topics);

        // ─── COMMENTS ─────────────────────────────────────────────────
        var comments = new List<Comment>
        {
            new Comment
            {
                Id = 1,
                Body = ".NET 8 with minimal APIs is fantastic. Performance is great, community is active. Definitely worth it in 2026!",
                AuthorId = 1,
                TopicId = 1,
                CreatedAt = new DateTime(2025, 4, 1, 13, 0, 0, DateTimeKind.Utc)
            },
            new Comment
            {
                Id = 2,
                Body = "I switched from Node to .NET Core 3 years ago and never looked back. Especially for enterprise — typing, DI, EF Core are all top-notch.",
                AuthorId = 3,
                TopicId = 1,
                CreatedAt = new DateTime(2025, 4, 2, 9, 20, 0, DateTimeKind.Utc)
            },
            new Comment
            {
                Id = 3,
                Body = "Angular gives you a full opinionated framework which helps large teams stay consistent. React is great but you have to make too many architectural decisions yourself.",
                AuthorId = 2,
                TopicId = 2,
                CreatedAt = new DateTime(2025, 4, 11, 14, 30, 0, DateTimeKind.Utc)
            },
            new Comment
            {
                Id = 4,
                Body = "Try DOU Jobs — it has many part-time/remote listings specifically for Ukrainian IT market.",
                AuthorId = 1,
                TopicId = 3,
                CreatedAt = new DateTime(2025, 4, 19, 10, 0, 0, DateTimeKind.Utc)
            },
            new Comment
            {
                Id = 5,
                Body = "I released a small puzzle game last year. Revenue is modest but the experience is invaluable. Just do it!",
                AuthorId = 3,
                TopicId = 4,
                CreatedAt = new DateTime(2025, 5, 3, 15, 45, 0, DateTimeKind.Utc)
            },
            new Comment
            {
                Id = 6,
                Body = "I use Clean Architecture with layers: Domain, Application, Infrastructure, API. Overkill for small projects but great for teams.",
                AuthorId = 2,
                TopicId = 5,
                CreatedAt = new DateTime(2025, 5, 15, 9, 0, 0, DateTimeKind.Utc)
            }
        };
        context.Comments.AddRange(comments);

        // ─── NEWS ARTICLES ────────────────────────────────────────────
        var news = new List<NewsArticle>
        {
            new NewsArticle
            {
                Id = 1,
                Title = "Ukrainian developers are increasingly choosing remote-first work",
                Summary = "A new survey of 3,000 Ukrainian IT professionals shows that over 80% now prefer fully remote positions.",
                Body = "According to a recent survey conducted across major Ukrainian developer communities, more than 80% of respondents stated they would prefer a fully remote position over on-site work, even when salaries are comparable. The trend accelerated significantly after 2022 and shows no signs of reversing. Companies adapting to this new reality are offering better async communication tooling, flexible hours, and home office stipends.",
                Category = "Ukraine IT",
                AuthorName = "Редакція DevTalk",
                PublishedAt = new DateTime(2025, 5, 10, 10, 0, 0, DateTimeKind.Utc),
                ViewCount = 890
            },
            new NewsArticle
            {
                Id = 2,
                Title = "ASP.NET Core remains popular for enterprise backend",
                Summary = "Stack Overflow Developer Survey 2025 shows ASP.NET Core in top 5 most-used web frameworks globally.",
                Body = "The Stack Overflow Developer Survey 2025 confirmed what many .NET developers already knew: ASP.NET Core continues to rank among the top 5 web frameworks globally. Its combination of performance, strong typing, built-in dependency injection, and robust EF Core ORM make it a natural choice for enterprise backend teams. Microsoft's continued investment in .NET 9 and beyond signals a healthy future for the ecosystem.",
                Category = ".NET",
                AuthorName = "Admin DevTalk",
                PublishedAt = new DateTime(2025, 4, 22, 14, 0, 0, DateTimeKind.Utc),
                ViewCount = 634
            },
            new NewsArticle
            {
                Id = 3,
                Title = "Indie developers use Steam as a side-income platform",
                Summary = "Growing number of Eastern European developers release small games on Steam as a supplementary income stream.",
                Body = "Steam's low barrier to entry and global reach have made it an attractive platform for developers looking to monetize side projects. Several Ukrainian and Polish developers have reported earning between $500–$5,000 per month from small indie titles. The most successful strategies involve building small but highly polished experiences, investing in localization, and engaging the community early through wishlist campaigns.",
                Category = "Gamedev",
                AuthorName = "OleksandrK",
                PublishedAt = new DateTime(2025, 3, 30, 11, 30, 0, DateTimeKind.Utc),
                ViewCount = 421
            },
            new NewsArticle
            {
                Id = 4,
                Title = "React and TypeScript dominate frontend pet projects",
                Summary = "An analysis of GitHub repositories shows React + TypeScript is the most popular stack for personal and portfolio projects in 2025.",
                Body = "An analysis of over 50,000 public GitHub repositories tagged with 'portfolio' or 'pet project' reveals that React combined with TypeScript is the overwhelming choice for frontend developers building personal projects. Vite has largely replaced Create React App as the default build tool, and React Router v6+ is used in the majority of multi-page projects. CSS Modules and Tailwind CSS are neck-and-neck for styling.",
                Category = "Frontend",
                AuthorName = "Редакція DevTalk",
                PublishedAt = new DateTime(2025, 5, 1, 9, 0, 0, DateTimeKind.Utc),
                ViewCount = 758
            },
            new NewsArticle
            {
                Id = 5,
                Title = "Remote .NET developer salaries in Ukraine up 15% in 2025",
                Summary = "Compensation data from DOU shows mid-level .NET developers earning average of $3,200/month.",
                Body = "According to DOU's bi-annual salary survey, mid-level .NET developers in Ukraine saw a 15% increase in average monthly compensation in 2025, reaching approximately $3,200 USD. Senior .NET engineers with cloud experience command $5,000+ remotely. The demand for ASP.NET Core and Azure expertise continues to outpace the supply of qualified candidates.",
                Category = "Career",
                AuthorName = "IvanRecruiter",
                PublishedAt = new DateTime(2025, 4, 15, 8, 0, 0, DateTimeKind.Utc),
                ViewCount = 1102
            }
        };
        context.NewsArticles.AddRange(news);

        context.SaveChanges();
    }
}
