using System.Collections.Generic;
using System.Linq;
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
            if (source == Provider.Cinemaworld.ToString())
            {
                MovieDetails cinemaWorldMovieDetails = await _cinemaWorldService.GetMovieDetails(id, Provider.Cinemaworld.ToString());
                cinemaWorldMovieDetails.Provider = Provider.Cinemaworld;
                return cinemaWorldMovieDetails;
            }
            MovieDetails filmWorldMovieDetails = await _filmWorldService.GetMovieDetails(id, Provider.Filmworld.ToString());
            filmWorldMovieDetails.Provider = Provider.Filmworld;
            return filmWorldMovieDetails;
        }

        public async Task<List<CombinedMovie>> GetMovies()
        {
            _logger.LogInformation("Calling GetMovies from MovieService");
            List<Movie> cinemaWorldMovies = await _cinemaWorldService.GetMovies();
            List<Movie> filmWorldMovies = await _filmWorldService.GetMovies();
            List<CombinedMovie> combinedMovieList = new List<CombinedMovie>();
            var combinedMovies = cinemaWorldMovies.Union(filmWorldMovies, new MovieComparer()).ToList();
            foreach (var movie in combinedMovies)
            {
                combinedMovieList.Add(new CombinedMovie()
                {
                    CinemaWorldId = cinemaWorldMovies.Find(movie1 => movie1.Title.Equals(movie.Title))?.Id,
                    FilmWorldId = filmWorldMovies.Find(movie1 => movie1.Title.Equals(movie.Title))?.Id,
                    Poster = movie.Poster,
                    Title = movie.Title,
                    Type = movie.Type,
                    Year = movie.Year
                });
            }
            return combinedMovieList;
        }
    }

    public class MovieComparer : IEqualityComparer<Movie>
    {
        public bool Equals(Movie x, Movie y)
        {
            return y != null && x != null && x.Title.Equals(y.Title);
        }
        public int GetHashCode(Movie obj)
        {
            return obj.Title.GetHashCode();
        }
    }
}
