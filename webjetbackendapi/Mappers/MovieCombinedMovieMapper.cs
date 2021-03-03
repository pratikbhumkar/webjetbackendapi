using AutoMapper;
using webjetbackendapi.Models;

namespace webjetbackendapi.Mappers
{
    public class MovieCombinedMovieMapper: Profile
    {
        public MovieCombinedMovieMapper()
        {
            CreateMap<Movie, CombinedMovie>();
        }
    }
}
