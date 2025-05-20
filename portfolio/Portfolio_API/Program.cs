using Microsoft.Extensions.Options;
using Octokit;
using Portfolio_service;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Config
builder.Services.Configure<GitHubIntegrationOption>(
    builder.Configuration.GetSection("GitHubIntegrationOption"));

builder.Services.AddGitHubIntegration(options =>
    builder.Configuration.GetSection(nameof(GitHubIntegrationOption)).Bind(options));

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient", policy =>
    {
        policy.WithOrigins("https://portfolioclient-3d8e.onrender.com")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// DI
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IGitHubService, GitHubService>();
builder.Services.Decorate<IGitHubService, CachingGitHubService>();

var app = builder.Build();

// Global error logging (לבדיקת שגיאות 500)
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Unhandled exception: {ex.Message}");
        throw;
    }
});

// Middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowClient"); // ? חשוב שיהיה לפני Authorization

app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "portfolio server running");

app.Run();
