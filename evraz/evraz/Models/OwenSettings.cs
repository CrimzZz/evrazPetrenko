namespace evraz.Models
{
    public class OwenSettings
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
