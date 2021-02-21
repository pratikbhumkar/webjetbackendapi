using NUnit.Framework;
using webjetbackendapi;
using Microsoft.Extensions.DependencyInjection;
using webjetbackendapi.Services.Interfaces;

namespace webjetbackendapitests
{
    [TestFixture]
    public class FilmWorldServiceTests
    {
        private ServiceProvider _serviceProvider;
        private IFilmWorldService _sut;

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
        public void TestMovieWorldServiceDoesntThrowError()
        {
            _sut = _serviceProvider.GetService<IFilmWorldService>();
            Assert.DoesNotThrow(() => _sut.GetMovies());
        }

    }
}
