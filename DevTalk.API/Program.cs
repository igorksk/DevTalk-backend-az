using DevTalk.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ─── Services ─────────────────────────────────────────────────────────────────

// Use EF Core In-Memory database — data lives in RAM and resets on restart
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("DevTalkDb"));

builder.Services.AddControllers();

// Enable Swagger UI for easy API testing during development
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "DevTalk UA API", Version = "v1" });
});

// Allow React dev server (localhost:5173) to call the API
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173", "https://orange-flower-09368240f.7.azurestaticapps.net/")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// ─── Seed Data ────────────────────────────────────────────────────────────────
// Populate the in-memory DB with demo data every time the app starts
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    SeedData.Initialize(db);
}

// ─── Middleware Pipeline ──────────────────────────────────────────────────────

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("DevCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
