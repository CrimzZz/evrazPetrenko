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
using evraz.Data.DbEntities;
using evraz.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

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
                    var serviceScopeFactory = (IServiceScopeFactory)_serviceProvider.GetService(typeof(IServiceScopeFactory));
                    using (var scope = serviceScopeFactory.CreateScope())
                    {
                        try
                        {
                            _logger.LogInformation("Saving to db...");
                            var services = scope.ServiceProvider;
                            var dbContext = services.GetRequiredService<ApplicationDbContext>();
                            var raport = JsonSerializer.Deserialize<Raport>(message);
                            raport.FormPlace = "Отправка загатовок";
                            raport.FormDate = DateTime.Now;
                            raport.Responsables = "Fio";
                            dbContext.Raports.Add(raport);
                            dbContext.SaveChanges();
                            _logger.LogInformation("Saved");
                        }
                        catch
                        {
                            _logger.LogError("something went wrong");
                        }
                    }
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
