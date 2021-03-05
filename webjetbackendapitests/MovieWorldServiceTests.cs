using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using webjetbackendapi;
using webjetbackendapi.Models;
using webjetbackendapi.Services.Interfaces;

namespace webjetbackendapitests
{
    [TestFixture]
    public class MovieWorldServiceTests
    {
        private ServiceProvider _serviceProvider;
        private IMovieService _sut;

        [SetUp]
        public void Setup()
        {
            var configuration = Configuration.Generate();
            var startup = new Startup(configuration);
            ServiceCollection serviceCollection = new ServiceCollection();
            startup.ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [Test]
        public void TestMovieWorldServiceGetMoviesDoesntThrowError()
        {
            _sut = _serviceProvider.GetService<IMovieService>();
            Assert.DoesNotThrowAsync(() => _sut.GetCombinedMoviesAsync());
        }
        [Test]
        public void TestMovieWorldServiceGetMovieDetailsDoesntThrowError()
        {
            _sut = _serviceProvider.GetService<IMovieService>();
            Assert.DoesNotThrowAsync(() => _sut.GetProcessedMovieDetailsAsync("cw0120915", "Cinemaworld"));
        }
        [Test]
        public async Task TestMovieWorldServiceGetMovieDetailsDoesAddSourceDetails()
        {
            _sut = _serviceProvider.GetService<IMovieService>();
            MovieDetails result = await _sut.GetProcessedMovieDetailsAsync("cw0120915", "Cinemaworld");
            Assert.AreEqual(result.Provider, "Cinemaworld");
        }
        [Test]
        public async Task TestMovieWorldServiceGetMovieDetailsReturnsMovies()
        {
            _sut = _serviceProvider.GetService<IMovieService>();
            var movies = await _sut.GetCombinedMoviesAsync();
            Assert.IsTrue(movies.Any());
            Assert.IsInstanceOf<CombinedMovie>(movies[0]);
        }

        [Test]
        public async Task TestMovieWorldServiceGetMovieDetailsReturnsCombinedCombinedMoviesWithIds()
        {
            _sut = _serviceProvider.GetService<IMovieService>();
            var movies = await _sut.GetCombinedMoviesAsync();
            if (String.IsNullOrEmpty(movies[0].CinemaWorldId)  && String.IsNullOrEmpty(movies[0].FilmWorldId))
            {
                Assert.Fail("The combined result does not have a CinemaWorldId or FilmWorldId");
            }
        }
    }
}
