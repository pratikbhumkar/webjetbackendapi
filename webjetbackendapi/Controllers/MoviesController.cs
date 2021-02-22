using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using webjetbackendapi.Services.Interfaces;

namespace webjetbackendapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly IMovieService _movieService;
        public MoviesController(ILogger<MoviesController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _logger.LogInformation("In MoviesController Getting movies");
                var movieList = await _movieService.GetMovies();
                return Ok(movieList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("GetMovie/{source}/{id}")]
        public async Task<IActionResult> GetMoviesById(string source, string id)
        {
            try
            {
                _logger.LogInformation("In MoviesController Getting movie details");
                var movieDetails = await _movieService.GetMovieDetails(id, source);
                return Ok(movieDetails);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}