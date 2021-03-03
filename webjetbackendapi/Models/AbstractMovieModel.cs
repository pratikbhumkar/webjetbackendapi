namespace webjetbackendapi.Models
{
    public enum Provider
    {
        Cinemaworld,
        Filmworld
    }
    public abstract class AbstractMovieModel
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
    }
}
