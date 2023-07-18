using ControlService.Workers;
using db;
using FreezerService.Workers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Profiler.Workers;
using RecieverService.Workers;
using SenderService.Workers;
using SenderServices.Workers;
using StartWorkers.Interfaces;
using StartWorkers.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartWorkers
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

            

            
            services.AddSingleton<IServiceSettings, ControlSettings>();
            services.AddSingleton<IServiceSettings, FreezerSettings>();
            services.AddSingleton<IServiceSettings, OwenSettings>();
            services.AddSingleton<IServiceSettings, ProfilerSettings>();
            services.AddSingleton<IServiceSettings, RecieverSettings>();
            services.AddSingleton<IServiceSettings, SenderSettings>();
            services.AddScoped<PublisherRecieverWorker>();
            services.AddScoped<ConsumerRecieverWorker>();
            services.AddScoped<PublisherProfilerWorker>();
            services.AddScoped<ConsumerProfilerWorker>();
            services.AddScoped<PublisherControlWorker>();
            services.AddScoped<ConsumerControlWorker>();
            services.AddScoped<PublisherFreezerWorker>();
            services.AddScoped<ConsumerFreezerWorker>();
            services.AddScoped<PublisherSenderWorker>();
            services.AddScoped<ConsumerSenderWorker>();
        }
    }
}
