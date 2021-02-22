using System;
using System.Threading.Tasks;
using webjetbackendapi.Services.Interfaces;
using webjetbackendapi.Models;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using webjetbackendapi.Exceptions;
using webjetbackendapi.Gateway;

namespace webjetbackendapi.Services
{
    public class FilmWorldService : IFilmWorldService
    {
        private readonly ILogger<FilmWorldService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMovieServiceGateway _movieServiceGateway;

        public FilmWorldService(ILogger<FilmWorldService> logger,
            IConfiguration configuration, IMovieServiceGateway movieServiceGateway)
        {
            _logger = logger;
            _configuration = configuration;
            _movieServiceGateway = movieServiceGateway;
        }

        public async Task<MovieDetails> GetMovieDetails(string id, string source)
        {
            _logger.Log(LogLevel.Information, "Getting movie details");
            var url = $"{_configuration.GetSection("filmworldmoviedetailsextension").Value}/{id}";
            var content = await _movieServiceGateway.GetDetailsFromServer(url);
            var movieDetails = JsonConvert.DeserializeObject<MovieDetails>(content);
            return movieDetails;
        }

        public async Task<List<Movie>> GetMovies()
        {
            _logger.Log(LogLevel.Information, "Getting all movies");
            var content = await _movieServiceGateway.GetDetailsFromServer(_configuration.GetSection("filmworldmoviesextension").Value);
            var movieList = JsonConvert.DeserializeObject<MovieResponse>(content);
            return movieList.Movies;
        }
    }
}
