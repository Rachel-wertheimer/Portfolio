using Microsoft.Extensions.Options;
using Octokit;
using Portfolio_service;

var builder = WebApplication.CreateBuilder(args);

// ����� �������
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// �����������
builder.Services.Configure<GitHubIntegrationOption>(
    builder.Configuration.GetSection("GitHubIntegrationOption"));

builder.Services.AddGitHubIntegration(options =>
    builder.Configuration.GetSection(nameof(GitHubIntegrationOption)).Bind(options));

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile("/etc/secrets/github-secret.json", optional: true, reloadOnChange: true); // ����!


// CORS - ���� ���� ������ �-Origin ���� ������
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

// Middleware ������ ������� �������
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Unhandled exception: {ex.Message}");
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("Internal Server Error");
    }
});

// ����� Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// ��� ��: UseCors ���� ����� ���� UseAuthorization
app.UseCors("AllowClient");

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "portfolio server running");

app.Run();
