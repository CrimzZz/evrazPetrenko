namespace evraz.Models
{
    public class ProfilerSettings
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
