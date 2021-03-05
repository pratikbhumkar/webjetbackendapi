using System.Collections.Generic;
using webjetbackendapi.Models;

namespace webjetbackendapi.Extensions
{
    public class MovieComparer : IEqualityComparer<Movie>
    {
        public bool Equals(Movie x, Movie y)
        {
            return y != null && x != null && x.Title.Equals(y.Title);
        }
        public int GetHashCode(Movie obj)
        {
            return obj.Title.GetHashCode();
        }
    }
}
