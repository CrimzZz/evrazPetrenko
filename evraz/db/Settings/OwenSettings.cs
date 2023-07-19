using Microsoft.Extensions.Configuration;
using db.Interfaces;


namespace db.Settings
{
    public class OwenSettings : IServiceSettings
    {
        private readonly IConfiguration _configuration;

        public OwenSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Queue => _configuration["RabbitMQ:Queues:Owen"];
        public string PublishTo => _configuration["RabbitMQ:Queues:Profiler"];
        public string Host => _configuration["RabbitMQ:Host"];

    }
}
