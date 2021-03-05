using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using webjetbackendapi.Exceptions;
using webjetbackendapi.Extensions;
using webjetbackendapi.Models;
using webjetbackendapi.Services.Interfaces;
using InvalidDataException = System.IO.InvalidDataException;

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
            switch (source)
            {
                case "Cinemaworld":
                    MovieDetails cinemaWorldMovieDetails = await _cinemaWorldService.GetMovieDetailsAsync(id, Provider.Cinemaworld.ToString());
                    cinemaWorldMovieDetails.Provider = Provider.Cinemaworld.ToString();
                    return cinemaWorldMovieDetails;
                case "Filmworld":
                    MovieDetails filmWorldMovieDetails = await _filmWorldService.GetMovieDetailsAsync(id, Provider.Filmworld.ToString());
                    filmWorldMovieDetails.Provider = Provider.Filmworld.ToString();
                    return filmWorldMovieDetails;
                default:
                    throw new InvalidSourceException($"Invalid source value-{source}");
            }
        }

        public async Task<List<CombinedMovie>> GetCombinedMoviesAsync()
        {
            _logger.LogInformation("Calling GetCombinedMoviesAsync from MovieService.");
            List<Movie> cinemaWorldMovies;
            List<Movie> filmWorldMovies;
            try
            {
                /*
                 * Here I am running both the tasks at the same time, halving the time taken
                 */

                var cinemaWorldTask = _cinemaWorldService.GetMoviesAsync();
                var filmWorldTask = _filmWorldService.GetMoviesAsync();

                List<Movie>[] result = await Task.WhenAll(cinemaWorldTask, filmWorldTask);
                cinemaWorldMovies = result[0];
                filmWorldMovies = result[1];
            }
            catch (Exception)
            {
                throw new InvalidDataException("Data returned from the server is invalid/Data " +
                                               "cannot be converted");
            }
            /*
             * Here I am creating a combined object where I am merging the objects based on their Title.
             *I will be then finding their Ids and adding them to the object.
             */
            List <CombinedMovie> combinedMovieList = new List<CombinedMovie>();
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
}
