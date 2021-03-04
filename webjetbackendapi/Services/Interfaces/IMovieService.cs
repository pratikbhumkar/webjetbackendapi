using System.Collections.Generic;
using System.Threading.Tasks;
using webjetbackendapi.Models;

namespace webjetbackendapi.Services.Interfaces
{
    public interface IMovieService
    {
        Task<List<CombinedMovie>> GetCombinedMoviesAsync();
        Task<MovieDetails> GetProcessedMovieDetailsAsync(string id, string source);
    }
}
