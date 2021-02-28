using System.IO;
using Microsoft.Extensions.Configuration;

namespace webjetbackendapi
{
    public static class Configuration
    {
        public static IConfiguration Generate()
        {
            // Last loaded key will be used when there is an overlap
            var builder = new ConfigurationBuilder();
            
            return builder
                .AddJsonFile($"{Directory.GetCurrentDirectory()}/appsettings.json")
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
