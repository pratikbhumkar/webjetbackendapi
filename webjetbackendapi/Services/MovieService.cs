using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webjetbackendapi.Models;
using webjetbackendapi.Services.Interfaces;

namespace webjetbackendapi.Services
{
    public class MovieService : IMovieService
    {
        private ICinemaWorldService _cinemaWorldService;
        private IFilmWorldService _filmWorldService;

        public MovieService(ICinemaWorldService cinemaWorldService, IFilmWorldService filmWorldService)
        {
            _cinemaWorldService = cinemaWorldService;
            _filmWorldService = filmWorldService;
        }
        public Task<MovieDetails> GetMovieDetails(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Movie>> GetMovies()
        {
            var cinemaworldMovies = await _cinemaWorldService.GetMovies();
            var filmworldMovies = await _filmWorldService.GetMovies();
            return (List<Movie>) cinemaworldMovies.Concat(filmworldMovies);
        }
    }
}
