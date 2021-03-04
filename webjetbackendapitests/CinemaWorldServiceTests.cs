using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using webjetbackendapi;
using Microsoft.Extensions.DependencyInjection;
using webjetbackendapi.Models;
using webjetbackendapi.Services.Interfaces;

namespace webjetbackendapitests
{
    [TestFixture]
    public class CinemaWorldServiceTests
    {
        private ServiceProvider _serviceProvider;
        private ICinemaWorldService _sut;

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
        public void TestCinemaWorldServiceGetMoviesDoesntThrowError()
        {
            _sut = _serviceProvider.GetService<ICinemaWorldService>();
            Assert.DoesNotThrowAsync(()=>_sut.GetMoviesAsync());
        }
        [Test]
        public void TestCinemaWorldServiceGetMovieDetailsDoesntThrowError()
        {
            _sut = _serviceProvider.GetService<ICinemaWorldService>();
            Assert.DoesNotThrowAsync(() => _sut.GetMovieDetailsAsync("Cinemaworld", "cw0120915"));
        }
        [Test]
        public async Task TestCinemaWorldServiceGetMovieDetailsReturnsMovies()
        {
            _sut = _serviceProvider.GetService<ICinemaWorldService>();
            var movies = await _sut.GetMoviesAsync();
            Assert.IsTrue(movies.Any());
            Assert.IsInstanceOf<Movie>(movies[0]);
        }
    }
}