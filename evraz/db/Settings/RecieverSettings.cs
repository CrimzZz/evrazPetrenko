using Microsoft.Extensions.Configuration;
using db.Interfaces;


namespace db.Settings
{
    public class RecieverSettings : IServiceSettings
    {
        private readonly IConfiguration configuration;

        public RecieverSettings(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Queue => configuration["RabbitMQ:Queues:Reciever"];
        public string PublishTo => configuration["RabbitMQ:Queues:Owen"];
        public string Host => configuration["RabbitMQ:Host"];
    }
}
