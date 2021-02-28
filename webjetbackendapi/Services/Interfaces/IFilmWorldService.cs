using System.Collections.Generic;
using System.Threading.Tasks;
using webjetbackendapi.Models;

namespace webjetbackendapi.Services.Interfaces
{
    public interface IFilmWorldService
    {
        Task<List<Movie>> GetMovies();
        Task<MovieDetails> GetMovieDetails(string id, string source);
    }
}
