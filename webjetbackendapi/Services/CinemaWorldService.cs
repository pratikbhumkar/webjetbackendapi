using System.Collections.Generic;
using System.Threading.Tasks;
using webjetbackendapi.Models;
using webjetbackendapi.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using webjetbackendapi.Gateway;

namespace webjetbackendapi.Services
{
    public class CinemaWorldService : ICinemaWorldService
    {
        private readonly ILogger<CinemaWorldService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMovieServiceGateway _movieServiceGateway;
        public CinemaWorldService(ILogger<CinemaWorldService> logger, 
            IConfiguration configuration, IMovieServiceGateway movieServiceGateway)
        {
            _logger = logger;
            _configuration = configuration;
            _movieServiceGateway = movieServiceGateway;
        }
        public async Task<MovieDetails> GetMovieDetails(string id, string source)
        {
            _logger.LogInformation("Getting movie details");
            var url = $"{_configuration.GetSection("cinemaworldmoviedetailsextension").Value}/{id}";
            var content = await _movieServiceGateway.GetDetailsFromServer(url);
            var movieDetails = JsonConvert.DeserializeObject<MovieDetails>(content);
            return movieDetails;
        }

        public async Task<List<Movie>> GetMovies()
        {
            _logger.LogInformation("Getting all movies");
            var content = await _movieServiceGateway.GetDetailsFromServer(_configuration.GetSection("cinemaworldmoviesextension").Value);
            var movieList = JsonConvert.DeserializeObject<MovieResponse>(content);
            return movieList.Movies;
        }
    }
}
