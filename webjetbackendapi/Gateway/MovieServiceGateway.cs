using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Polly;
using webjetbackendapi.Exceptions;

namespace webjetbackendapi.Gateway
{
    public class MovieServiceGateway : IMovieServiceGateway
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MovieServiceGateway> _logger;
        public MovieServiceGateway(HttpClient httpClient, ILogger<MovieServiceGateway> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<string> GetDetailsFromServer(string source)
        {
            var result = string.Empty;
            var policy = Policy
                .Handle<FetchException>()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(10),
                    TimeSpan.FromSeconds(20),
                    TimeSpan.FromSeconds(30)
                });
            await policy.ExecuteAsync(async () =>
            {
                result = await CallServer(source);
            });
            return result;
        }

        private async Task<string> CallServer(string source)
        {
            try
            {
                var response = await _httpClient.GetAsync(source);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to fetch data from CinemaWorld-GetMovies", ex);
                throw new FetchException("Failed to fetch");
            }
        }
    }
}
