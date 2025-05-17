using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Octokit;
using Portfolio_service;


public class CachingGitHubService : IGitHubService
{
    private readonly IGitHubService _innerService;
    private readonly IMemoryCache _cache;
    private readonly GitHubClient _gitHubClient;
    private readonly GitHubIntegrationOption _options;
    private readonly ILogger<CachingGitHubService> _logger;

    private const string ReposCacheKey = "GitHubRepositories";
    private const string LastCommitShaKey = "GitHubLastCommitSha";

    public CachingGitHubService(
        IGitHubService innerService,
        IMemoryCache cache,
        IOptions<GitHubIntegrationOption> options,
        ILogger<CachingGitHubService> logger)
    {
        _innerService = innerService ?? throw new ArgumentNullException(nameof(innerService));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));

        if (string.IsNullOrEmpty(_options.Token) || string.IsNullOrEmpty(_options.UserName))
            throw new ArgumentException("GitHub token or username is missing.");

        _gitHubClient = new GitHubClient(new ProductHeaderValue("portfolio-app"))
        {
            Credentials = new Credentials(_options.Token)
        };
    }

    public async Task<List<RepositoryDTO>> GetMyRepositoriesAsync()
    {
        var latestSha = await GetLatestCommitShaAsync();
        var cachedSha = _cache.Get<string>(LastCommitShaKey);

        if (_cache.TryGetValue(ReposCacheKey, out List<RepositoryDTO> cachedRepos) && latestSha == cachedSha)
        {
            _logger.LogInformation("Returning repositories from cache.");
            return cachedRepos;
        }

        _logger.LogInformation("Fetching repositories from GitHub...");
        var repositories = await _innerService.GetMyRepositoriesAsync();

        _cache.Set(ReposCacheKey, repositories, TimeSpan.FromMinutes(30));
        _cache.Set(LastCommitShaKey, latestSha, TimeSpan.FromMinutes(30));

        return repositories;
    }

    public async Task<List<RepositoryDTO>> SeacrhMyRepositoriesAsync(string language)
    {
        var allRepos = await GetMyRepositoriesAsync();
        return allRepos
            .Where(r => r.Language.Equals(language, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    private async Task<string> GetLatestCommitShaAsync()
    {
        var repos = await _gitHubClient.Repository.GetAllForUser(_options.UserName);
        foreach (var repo in repos)
        {
            var commits = await _gitHubClient.Repository.Commit.GetAll(_options.UserName, repo.Name);
            var latestCommit = commits.FirstOrDefault();
            if (latestCommit != null)
                return latestCommit.Sha;
        }

        return string.Empty;
    }
}
