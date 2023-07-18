using Microsoft.Extensions.Configuration;
using db.Interfaces;

namespace StartWorkers.Settings
{
    public class ControlSettings : IServiceSettings
    {
        private readonly IConfiguration _configuration;

        public ControlSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Queue => _configuration["RabbitMQ:Queues:Control"];
        public string PublishTo => _configuration["RabbitMQ:Queues:Sender"];
        public string Host => _configuration["RabbitMQ:Host"];
    }
}
