namespace evraz.Models
{
    public class FreezerSettings
    {
        private readonly IConfiguration _configuration;

        public FreezerSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Queue => _configuration["RabbitMQ:Queues:Freezer"];
        public string PublishTo => _configuration["RabbitMQ:Queues:Control"];
        public string Host => _configuration["RabbitMQ:Host"];
    }
}
