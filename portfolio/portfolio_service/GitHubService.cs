using Microsoft.Extensions.Options;
using Octokit;

namespace Portfolio_service
{
    public class GitHubService : IGitHubService
    {
        private readonly GitHubClient _gitHubClient;
        private readonly GitHubIntegrationOption _options;

        public GitHubService(IOptions<GitHubIntegrationOption> options)
        {
            _options = options.Value;
            _gitHubClient = new GitHubClient(new ProductHeaderValue("my-github-app"))
            {
                Credentials = new Credentials(_options.Token)
            };
        }

        public async Task<List<RepositoryDTO>> GetMyRepositoriesAsync()
        {
            var repositories = await _gitHubClient.Repository.GetAllForUser(_options.UserName);
            var repoList = new List<RepositoryDTO>();

            foreach (var repo in repositories)
            {
                string presentationUrl = await GetPresentationFileUrl(repo.Name);

                repoList.Add(new RepositoryDTO
                {
                    Name = repo.Name,
                    Language = repo.Language ?? "Unknown",
                    Url = repo.HtmlUrl,
                    Description = repo.Description ?? "No description",
                    Status = repo.StargazersCount,
                    PresentationUrl = presentationUrl
                });
            }

            return repoList;
        }

        public async Task<List<RepositoryDTO>> SeacrhMyRepositoriesAsync(string language)
        {
            var repositories = await _gitHubClient.Repository.GetAllForUser(_options.UserName);
            var repoList = new List<RepositoryDTO>();

            foreach (var repo in repositories)
            {
                if (repo.Language != null && repo.Language.Equals(language, StringComparison.OrdinalIgnoreCase))
                {
                    string presentationUrl = await GetPresentationFileUrl(repo.Name);

                    repoList.Add(new RepositoryDTO
                    {
                        Name = repo.Name,
                        Description = repo.Description ?? "אין תיאור",
                        Language = repo.Language ?? "לא צוין",
                        Url = repo.HtmlUrl,
                        Status = repo.StargazersCount,
                        PresentationUrl = presentationUrl
                    });
                }
            }

            return repoList;
        }

        public async Task<List<RepositoryDTO>> SearchRepositoriesAsync(string language)
        {
            var lang = Enum.TryParse<Language>(language, true, out var languageEnum) ? languageEnum : Language.Unknown;
            var request = new SearchRepositoriesRequest("language") { Language = lang };
            var result = await _gitHubClient.Search.SearchRepo(request);

            var repoList = new List<RepositoryDTO>();

            foreach (var repo in result.Items)
            {
                string presentationUrl = await GetPresentationFileUrl(repo.Name);

                repoList.Add(new RepositoryDTO
                {
                    Name = repo.Name,
                    Description = repo.Description ?? "אין תיאור",
                    Language = repo.Language ?? "לא צוין",
                    Url = repo.HtmlUrl,
                    Status = repo.StargazersCount,
                    PresentationUrl = presentationUrl
                });
            }

            return repoList;
        }

        private async Task<string> GetPresentationFileUrl(string repoName)
        {
            try
            {
                var contents = await _gitHubClient.Repository.Content.GetAllContents(_options.UserName, repoName);

                var presentationFile = contents.FirstOrDefault(file =>
                    file.Name.EndsWith(".wmv") ||
                    file.Name.EndsWith(".mp4") ||
                    file.Name.EndsWith(".pptx") ||
                    file.Name.EndsWith(".pdf")
                );

                return presentationFile?.DownloadUrl;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
