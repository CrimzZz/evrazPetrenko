using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using db;
using db.DbEntities;
using db.Interfaces;

namespace FreezerService.Workers
{
    public class ConsumerFreezerWorker
    {
        private readonly ILogger<ConsumerFreezerWorker> _logger;
        private readonly IServiceSettings _settings;
        private readonly IServiceProvider _serviceProvider;

        public ConsumerFreezerWorker(ILogger<ConsumerFreezerWorker> logger, IServiceProvider services, IServiceSettings settings)
        {
            _logger = logger;
            _serviceProvider = services;
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

            _logger.LogInformation(" [*] Waiting for messages.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var serviceScopeFactory = (IServiceScopeFactory)_serviceProvider.GetService(typeof(IServiceScopeFactory));
                _logger.LogInformation($" [x] Received {message}");
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    try 
                    {
                        _logger.LogInformation("Saving to db...");
                        var services = scope.ServiceProvider;
                        var dbContext = services.GetRequiredService<ApplicationDbContext>();
                        var raport = JsonSerializer.Deserialize<Raport>(message);
                        raport.FormPlace = "Приём на охлождение";
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
                
                
            };
            channel.BasicConsume(queue: "hello",
                                 autoAck: true,
                                 consumer: consumer);
        }
    }
}
