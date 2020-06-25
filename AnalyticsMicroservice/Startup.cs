using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnalyticsMicroservice.AServices;
using AnalyticsMicroservice.DataSubscriber;
using AnalyticsMicroservice.Models;
using AnalyticsMicroservice.RefinedDataRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AnalyticsMicroservice
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
            services.Configure<RefinedDataDatabaseConfiguration>(Configuration.GetSection("RefinedDatastoreDatabaseSettings"));
            services.AddTransient<IRefinedDataRepository,RefinedDataRepository.RefinedDataRepository>();
            services.Configure<RabbitMQConfiguration>(Configuration.GetSection("RabbitMq"));
            services.AddHostedService<DataSubscriber.DataSubscriber>();
            services.AddSingleton<AnalyticsService>();
            services.AddControllers();
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
