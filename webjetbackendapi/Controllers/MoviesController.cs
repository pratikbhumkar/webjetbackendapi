using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using webjetbackendapi.Services.Interfaces;

namespace webjetbackend.Controllers
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
        public IActionResult GetMovies()
        {
            try
            {
                _movieService.GetMovies();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetMoviesById(string id)
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}