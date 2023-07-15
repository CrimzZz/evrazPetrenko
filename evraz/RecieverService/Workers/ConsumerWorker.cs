using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using evraz.Models;
using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecieverService.Workers
{
    public class ConsumerWorker
    {
        private readonly ILogger<ConsumerWorker> _logger;
        private readonly RecieverSettings _settings;

        public ConsumerWorker(ILogger<ConsumerWorker> logger, RecieverSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        public async void Consumer()
        {
            var factory = new ConnectionFactory { HostName = _settings.Host };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: _settings.Queue,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            Console.WriteLine(" [*] Waiting for messages.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($" [x] Received {message}");
            };
            channel.BasicConsume(queue: "hello",
                                 autoAck: true,
                                 consumer: consumer);
        }
    }
}
