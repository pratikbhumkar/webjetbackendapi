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

namespace webjetbackendapi.Services
{
    public class FilmWorldService : IFilmWorldService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FilmWorldService> _logger;
        private readonly IConfiguration _configuration;

        public FilmWorldService(HttpClient httpClient, ILogger<FilmWorldService> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
        }

        public Task<MovieDetails> GetMovieDetails(string id)
        {
            return null;
        }

        public async Task<List<Movie>> GetMovies()
        {
            try
            {
                var response = await _httpClient.GetAsync(_configuration.GetSection("filmworldextension").Value);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                MovieResponse movieList;
                movieList = JsonConvert.DeserializeObject<MovieResponse>(content);
                return movieList.Movies;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to fetch data from filmworld-GetMovies", ex);
                throw new FetchException("Failed to fetch");
            }   
        }
    }
}
