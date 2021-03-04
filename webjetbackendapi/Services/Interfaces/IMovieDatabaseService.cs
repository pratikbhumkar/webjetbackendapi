using System.Collections.Generic;
using System.Threading.Tasks;
using webjetbackendapi.Models;

namespace webjetbackendapi.Services.Interfaces
{
    public interface IMovieDatabaseService
    {
        Task<List<Movie>> GetMoviesAsync();
        Task<MovieDetails> GetMovieDetailsAsync(string id, string source);
    }
}
