using DevTalk.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DevTalk.API.Data;

// EF Core DbContext — the main entry point for database access
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<ForumTopic> ForumTopics => Set<ForumTopic>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<NewsArticle> NewsArticles => Set<NewsArticle>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Define relationships explicitly for clarity
        modelBuilder.Entity<ForumTopic>()
            .HasOne(t => t.Author)
            .WithMany(u => u.Topics)
            .HasForeignKey(t => t.AuthorId);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Author)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.AuthorId);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Topic)
            .WithMany(t => t.Comments)
            .HasForeignKey(c => c.TopicId);
    }
}
