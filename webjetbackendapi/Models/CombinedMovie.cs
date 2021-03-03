namespace webjetbackendapi.Models
{
    public class CombinedMovie : AbstractMovieModel
    {
        public string CinemaWorldId { get; set; }
        public string FilmWorldId { get; set; }
    }
}
