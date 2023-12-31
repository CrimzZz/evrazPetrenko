﻿using Microsoft.Extensions.Configuration;
using db.Interfaces;


namespace db.Settings
{
    public class SenderSettings : IServiceSettings
    {
        private readonly IConfiguration _configuration;

        public SenderSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Queue => _configuration["RabbitMQ:Queues:Sender"];
        
        public string Host => _configuration["RabbitMQ:Host"];

        public string PublishTo => throw new NotImplementedException();
    }
}
