using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using webjetbackendapi.Models;
using webjetbackendapi.Services.Interfaces;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using webjetbackendapi.Exceptions;

namespace webjetbackendapi.Services
{
    public class CinemaWorldService : ICinemaWorldService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CinemaWorldService> _logger;
        private readonly IConfiguration _configuration;
        public CinemaWorldService(HttpClient httpClient, ILogger<CinemaWorldService> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
        }
        public Task<MovieDetails> GetMovieDetails(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Movie>> GetMovies()
        {
            try
            {
                var response = await _httpClient.GetAsync(_configuration.GetSection("cinemaworldextension").Value);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                MovieResponse movieList;
                    movieList = JsonConvert.DeserializeObject<MovieResponse>(content);
                    return movieList.Movies;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to fetch data from CinemaWorld-GetMovies", ex);
                throw new FetchException("Failed to fetch");
            }
        }
    }
}
