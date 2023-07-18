using ControlService.Workers;
using FreezerService.Workers;
using Microsoft.Extensions.DependencyInjection;
using RecieverService.Workers;
using SenderService.Workers;
using SenderServices.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartWorkers
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
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
