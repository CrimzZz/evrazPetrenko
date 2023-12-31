﻿using RabbitMQ.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Extensions.Configuration;
using db.DbEntities;
using db;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using db.Interfaces;
using db.Settings;

namespace SenderServices.Workers
{
    public class PublisherSenderWorker
    {
        private readonly ILogger<PublisherSenderWorker> _logger;
        private readonly IServiceSettings _settings;
        private readonly IServiceProvider _serviceProvider;

        public PublisherSenderWorker(ILogger<PublisherSenderWorker> logger, IServiceProvider services, IServiceSettings settings)
        {
            _logger = logger;
            _settings = services.GetRequiredService<SenderSettings>();
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
                            raport.FormPlace = "Отправка потребителю";
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
