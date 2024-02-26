using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Services;
using Newtonsoft.Json;
using NiceMovies.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NiceMovies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly string _apiKey = "f6f5a6a9";
        private readonly HttpClient _httpClient;

        public MovieController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        [Route("SearchByTitle")]
        public async Task<IActionResult> SearchMovies([FromQuery] string title)
        {
            var movies = await GetMoviesByTitleFromApi(title);
            if (movies == null || movies.Count == 0)
            {
                return NotFound();
            }

            return Ok(movies);
        }

        private async Task<List<Movie>> GetMoviesByTitleFromApi(string title)
        {
            var apiUrl = $"http://www.omdbapi.com/?apikey={_apiKey}&s={title}";
            var response = await _httpClient.GetStringAsync(apiUrl);
            var searchResult = JsonConvert.DeserializeObject<Searched>(response);
            if (searchResult != null && searchResult.search != null)
            {
                return searchResult.search;
            }
            return new List<Movie>();
        }
    }
}
