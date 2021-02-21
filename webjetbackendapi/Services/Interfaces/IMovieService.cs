using System.Collections.Generic;
using System.Threading.Tasks;
using webjetbackendapi.Models;

namespace webjetbackendapi.Services.Interfaces
{
    public interface IMovieService
    {
        Task<List<Movie>> GetMovies();
        Task<MovieDetails> GetMovieDetails(string id);
    }
}
