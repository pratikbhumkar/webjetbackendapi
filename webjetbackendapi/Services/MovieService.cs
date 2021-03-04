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
        private readonly IFilmWorldService _filmWorldService;
        private readonly ICinemaWorldService _cinemaWorldService;
        private readonly ILogger<MovieService> _logger;
        public MovieService(ICinemaWorldService cinemaWorldService, IFilmWorldService filmWorldService, ILogger<MovieService> logger)
        {
            _cinemaWorldService = cinemaWorldService;
            _filmWorldService = filmWorldService;
            _logger = logger;

        }
        public async Task<MovieDetails> GetProcessedMovieDetailsAsync(string id, string source)
        {
            _logger.LogInformation("Calling GetProcessedMovieDetailsAsync from MovieService");
            if (source == Provider.Cinemaworld.ToString())
            {
                MovieDetails cinemaWorldMovieDetails = await _cinemaWorldService.GetMovieDetailsAsync(id, Provider.Cinemaworld.ToString());
                cinemaWorldMovieDetails.Provider = Provider.Cinemaworld.ToString();
                return cinemaWorldMovieDetails;
            }
            MovieDetails filmWorldMovieDetails = await _filmWorldService.GetMovieDetailsAsync(id, Provider.Filmworld.ToString());
            filmWorldMovieDetails.Provider = Provider.Filmworld.ToString();
            return filmWorldMovieDetails;
        }

        public async Task<List<CombinedMovie>> GetCombinedMoviesAsync()
        {
            _logger.LogInformation("Calling GetCombinedMoviesAsync from MovieService.");
            List<Movie> cinemaWorldMovies = await _cinemaWorldService.GetMoviesAsync();
            List<Movie> filmWorldMovies = await _filmWorldService.GetMoviesAsync();
            /*Here I am creating a combined object where I am merging the objects based on their Title.
             *I will be then finding their Ids and adding them to the object.
             */
            List<CombinedMovie> combinedMovieList = new List<CombinedMovie>();
            List<Movie> combinedMovies = cinemaWorldMovies.Union(filmWorldMovies, new MovieComparer()).ToList();
            _logger.LogInformation("Merging movies to form Combined object.");
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
