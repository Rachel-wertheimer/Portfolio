using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_service
{
    public interface IGitHubService
    {
        public Task<List<RepositoryDTO>> GetMyRepositoriesAsync();
        public Task<List<RepositoryDTO>> SeacrhMyRepositoriesAsync(string language);

    }
}
