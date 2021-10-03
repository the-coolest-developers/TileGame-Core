using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Configuration;
using TileGameServer.InSession.Constants;
using TileGameServer.InSession.Extensions.DependencyInjection;
using WebApiBaseLibrary.Infrastructure.Configuration;
using WebApiBaseLibrary.Infrastructure.Extensions.RabbitMQ;

namespace TileGameServer.InSession
{
    public class Startup
    {
        private IServiceProvider _serviceProvider;

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var rabbitMqConfiguration = new RabbitMQConfiguration
            {
                HostName = Environment.GetEnvironmentVariable(EnvironmentVariables.RabbitMQHostName),
                Port = int.Parse(Environment.GetEnvironmentVariable(EnvironmentVariables.RabbitMQPort)!),
                VirtualHost = Environment.GetEnvironmentVariable(EnvironmentVariables.RabbitMQVirtualHost),
                UserName = Environment.GetEnvironmentVariable(EnvironmentVariables.RabbitMQUserName),
                Password = Environment.GetEnvironmentVariable(EnvironmentVariables.RabbitMQPassword)
            };

            services.AddMessageQueueingServices();
            services.AddRabbitMQ(rabbitMqConfiguration);

            services.AddMediatR(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _serviceProvider = app.ApplicationServices;

            app.UseMessageQueueingServices();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
            });
        }
    }
}