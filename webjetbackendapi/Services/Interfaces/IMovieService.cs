using System;
using System.Threading.Tasks;

namespace webjetbackendapi.Services.Interfaces
{
    public interface IMovieService
    {
        Task<Object> GetMovies();
        Task<Object> GetMovieDetails();
    }
}
