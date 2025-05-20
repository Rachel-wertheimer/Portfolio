using Microsoft.Extensions.Options;
using Octokit;
using Portfolio_service;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<GitHubIntegrationOption>(
    builder.Configuration.GetSection("GitHubIntegrationOption")
);

builder.Services.AddGitHubIntegration(options => builder.Configuration.GetSection(nameof(GitHubIntegrationOption)).Bind(options));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient", policy =>
    {
        policy.WithOrigins("https://portfolioclient-3d8e.onrender.com")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


builder.Services.AddMemoryCache();
builder.Services.AddScoped<IGitHubService, GitHubService>();
builder.Services.Decorate<IGitHubService, CachingGitHubService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseCors("AllowClient");


app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "portfolio server running");
app.Run();


