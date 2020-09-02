using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceMicroservice.CommandReceiver;
using DeviceMicroservice.DataPublisher;
using DeviceMicroservice.Repositories;
using DeviceMicroservice.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;




namespace DeviceMicroservice
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddTransient<IDataRepository, DataRepository>();
            services.AddControllers();
            services.Configure<RabbitMQConfiguration>(Configuration.GetSection("RabbitMq"));
            services.AddTransient<IDataPublisher, DataPublisher.DataPublisher>();
            services.AddSingleton<Sensors>();
            services.AddSingleton<IHostedService, Sensors>(serviceProvider => serviceProvider.GetService<Sensors>());
            services.AddHostedService<CommandReceiver.CommandReceiver>();
            services.AddSingleton<ReadService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
