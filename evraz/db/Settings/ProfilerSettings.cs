using Microsoft.Extensions.Configuration;
using db.Interfaces;


namespace db.Settings
{
    public class ProfilerSettings : IServiceSettings
    {
        private readonly IConfiguration _configuration;

        public ProfilerSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Queue => _configuration["RabbitMQ:Queues:Profiler"];
        public string PublishTo => _configuration["RabbitMQ:Queues:Freezer"];
        public string Host => _configuration["RabbitMQ:Host"];

    }
}
