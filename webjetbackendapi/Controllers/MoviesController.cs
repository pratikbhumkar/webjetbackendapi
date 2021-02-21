using System;
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
        public IActionResult Get()
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

        /*[HttpGet]
        public IActionResult GetMoviesById(string id)
        {
            try
            {
                _movieService.GetMovieDetails(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }*/
    }
}