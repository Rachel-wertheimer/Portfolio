using Microsoft.AspNetCore.Mvc;
using Portfolio_service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Portfolio_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IGitHubService _gitHubService;


        public PortfolioController(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }


        // GET: api/<PortfolioController>
        [HttpGet]
        public async Task<ActionResult<List<RepositoryDTO>>> GetMyRepositories()
        {
           
                try
                {
                    var repositories = await _gitHubService.GetMyRepositoriesAsync();
                    return repositories;
                }
                catch (Exception ex)
                {
                    // הדפסת השגיאה
                    Console.WriteLine(ex.Message);
                    return StatusCode(500, "Internal Server Error: " + ex.Message);
                
            }
        }

        // GET api/<PortfolioController>/5
        [HttpGet("searchMyRepo/{language}")]
        public async Task<ActionResult<List<RepositoryDTO>>> SeacrhMyRepositoriesAsync(string language)
        {
            var repositories = await _gitHubService.SeacrhMyRepositoriesAsync(language);
            return Ok(repositories);
        }
       
     
    }
}
