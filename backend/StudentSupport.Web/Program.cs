using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StudentSupport.BusinessLogic.Services;
using StudentSupport.BusinessLogic.Services.IServices;
using StudentSupport.DataAccess.Data;
using StudentSupport.DataAccess.Repositories;
using StudentSupport.DataAccess.Repositories.IRepositories;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Debug: print environment and connection string
Console.WriteLine("Environment: " + builder.Environment.EnvironmentName);
Console.WriteLine("Connection String: " + builder.Configuration.GetConnectionString("DefaultConnection"));

// Services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVite", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddScoped<IPostRepo, PostRepo>();
builder.Services.AddScoped<IQuestionRepo, QuestionRepo>();
builder.Services.AddScoped<IAnswerRepo, AnswerRepo>();
builder.Services.AddScoped<IAnswerUpvoteRepo, AnswerUpvoteRepo>();
builder.Services.AddScoped<IPointRepo, PointRepo>();

builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IAnswerService, AnswerService>();
builder.Services.AddScoped<IPointService, PointService>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning))
);

builder.Services.AddOpenApi();

var app = builder.Build();

// ✅ Only apply migrations in Development
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    if (app.Environment.IsDevelopment())
    {
        var maxRetries = 10;
        for (int i = 0; i < maxRetries; i++)
        {
            try
            {
                Console.WriteLine($"Database migration attempt {i + 1}/{maxRetries}...");
                if (await context.Database.CanConnectAsync())
                {
                    await context.Database.MigrateAsync();
                    Console.WriteLine("Database migrated successfully!");
                    break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Attempt {i + 1} failed: {ex.Message}");
                await Task.Delay(5000);
            }
        }
    }
    else
    {
        // Production: just check connectivity
        if (await context.Database.CanConnectAsync())
        {
            Console.WriteLine("Production DB connection successful. Skipping migrations.");
        }
        else
        {
            Console.WriteLine("Cannot connect to production database!");
        }
    }
}

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowVite");
app.UseAuthorization();
app.MapControllers();

app.MapGet("/health", () => new {
    status = "Healthy",
    timestamp = DateTime.UtcNow
});

Console.WriteLine("🚀 Application started successfully!");
app.Run();
