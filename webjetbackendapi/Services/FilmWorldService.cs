using System.Threading.Tasks;
using webjetbackendapi.Services.Interfaces;
using webjetbackendapi.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using webjetbackendapi.Gateway;

namespace webjetbackendapi.Services
{
    public class FilmWorldService : IFilmWorldService
    {
        private readonly ILogger<FilmWorldService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMovieServiceGateway _movieServiceGateway;
        private readonly IMemoryCache _memoryCache;
        public FilmWorldService(ILogger<FilmWorldService> logger, IMemoryCache memoryCache,
            IConfiguration configuration, IMovieServiceGateway movieServiceGateway)
        {
            _logger = logger;
            _configuration = configuration;
            _movieServiceGateway = movieServiceGateway;
            _memoryCache = memoryCache;
        }

        public async Task<MovieDetails> GetMovieDetailsAsync(string id, string source)
        {
            _logger.Log(LogLevel.Information, "Getting movie details");
            var url = $"{_configuration.GetSection("filmworldmoviedetailsextension").Value}/{id}";
            var content = await _movieServiceGateway.GetDetailsFromServer(url);
            var movieDetails = JsonConvert.DeserializeObject<MovieDetails>(content);
            return movieDetails;
        }

        public async Task<List<Movie>> GetMoviesAsync()
        {
            _logger.LogInformation("Getting all FilmWorld movies");
            if (!_memoryCache.TryGetValue(CacheKeys.FilmWorldMovieList, out List<Movie> cacheEntry))
            {
                _logger.LogInformation("Getting all movies");
                var content =
                    await _movieServiceGateway.GetDetailsFromServer(_configuration
                        .GetSection("filmworldmoviesextension").Value);
                var movieList = JsonConvert.DeserializeObject<MovieResponse>(content);
                _logger.LogInformation("Getting all movies from FilmWorld server-storing in cache");
                cacheEntry = movieList.Movies;
            }
            return cacheEntry;
        }
    }
}
