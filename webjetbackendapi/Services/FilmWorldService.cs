using System;
using System.Threading.Tasks;
using webjetbackendapi.Services.Interfaces;

namespace webjetbackendapi.Services
{
    public class FilmWorldService : IFilmWorldService
    {
        public Task<object> GetMovieDetails()
        {
            throw new NotImplementedException();
        }

        public Task<object> GetMovies()
        {
            return null;
        }
    }
}
