namespace webjetbackendapi.Models
{
    public enum Provider
    {
        Cinemaworld,
        Filmworld
    }

    public class Movie
    {
        public Provider Provider{
            get;
            set;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Poster { get; set; }
        public decimal? Price { get; set; }
    }
}
