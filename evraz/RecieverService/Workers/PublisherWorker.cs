using RabbitMQ.Client;
using Microsoft.Extensions.Logging;
using evraz.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace RecieverService.Workers
{
    public class PublisherWorker
    {
        private readonly ILogger<PublisherWorker> _logger;
        private readonly RecieverSettings _settings;

        public PublisherWorker(ILogger<PublisherWorker> logger, RecieverSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        public async void Publish()
        {
            var factory = new ConnectionFactory() { HostName = _settings.Host};
            using (var connection = factory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: _settings.Queue,
                           durable: false,
                           exclusive: false,
                           autoDelete: false,
                           arguments: null);
                    string message = 
                }
            }
        }
    }
}
