using System.Collections.Gene

namespace NiceMovies.Models
{
    public class SearchedResult
    {
        [JsonPropertyName("Search")]
        public List<Movie> Search { get; set; }
    }
}
