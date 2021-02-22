using System.Threading.Tasks;

namespace webjetbackendapi.Gateway
{
    public interface IMovieServiceGateway
    {
        public Task<string> GetDetailsFromServer(string source);
    }
}
