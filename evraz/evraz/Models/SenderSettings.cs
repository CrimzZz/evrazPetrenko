namespace evraz.Models
{
    public class SenderSettings
    {
        private readonly IConfiguration _configuration;

        public SenderSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Queue => _configuration["RabbitMQ:Queues:Sender"];
        
        public string Host => _configuration["RabbitMQ:Host"];
    }
}
