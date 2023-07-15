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
        private readonly IServiceProvider _serviceProvider;

        public PublisherWorker(ILogger<PublisherWorker> logger, IServiceProvider services, RecieverSettings settings)
        {
            _logger = logger;
            _settings = settings;
            _serviceProvider = services;
        }

        public async void Publish(string jsonObject)
        {
            _logger.LogInformation("Sending message...");
            var factory = new ConnectionFactory() { HostName = _settings.Host};
            using (var connection = factory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: _settings.PublishTo,
                           durable: false,
                           exclusive: false,
                           autoDelete: false,
                           arguments: null);
                    string message = jsonObject;
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: string.Empty,
                     routingKey: _settings.PublishTo,
                     basicProperties: null,
                     body: body);
                    _logger.LogInformation($" [x] Sent {message}");
                }
            }
        }
    }
}
