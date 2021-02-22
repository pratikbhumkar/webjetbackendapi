using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using webjetbackendapi.Models;
using webjetbackendapi.Services.Interfaces;

namespace webjetbackendapi.Services
{
    public class MovieService : IMovieService
    {
        private ICinemaWorldService _cinemaWorldService;
        private IFilmWorldService _filmWorldService;
        private readonly ILogger<MovieService> _logger;

        public MovieService(ICinemaWorldService cinemaWorldService, IFilmWorldService filmWorldService, ILogger<MovieService> logger)
        {
            _cinemaWorldService = cinemaWorldService;
            _filmWorldService = filmWorldService;
            _logger = logger;
        }
        public async Task<MovieDetails> GetMovieDetails(string id, string source)
        {
            _logger.LogInformation("Calling GetMovieDetails from MovieService");
            if (source == MovieSource.Source.CinemaWorld.ToString())
            {
                return await _cinemaWorldService.GetMovieDetails(id, MovieSource.Source.CinemaWorld.ToString());
            }
            return await _filmWorldService.GetMovieDetails(id, MovieSource.Source.FilmWorld.ToString());
        }

        public async Task<List<Movie>> GetMovies()
        {
            _logger.LogInformation("Calling GetMovies from MovieService");
            List<Movie> cinemaWorldMovies = await _cinemaWorldService.GetMovies();
            List<Movie> filmWorldMovies = await _filmWorldService.GetMovies();
            cinemaWorldMovies.AddRange(filmWorldMovies);
            return cinemaWorldMovies;
        }
    }
}
