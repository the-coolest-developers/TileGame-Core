using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TileGameServer.InSession.Constants;
using TileGameServer.InSession.Extensions.DependencyInjection;
using TileGameServer.InSession.HostedServices;
using WebApiBaseLibrary.Infrastructure.Configuration;
using WebApiBaseLibrary.Infrastructure.MessageQueueing.RabbitMQ.Extensions;

namespace TileGameServer.InSession
{
    public class Startup
    {
        private IServiceProvider _serviceProvider;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));

            var rabbitMqConfiguration = new RabbitMQConfiguration
            {
                HostName = Environment.GetEnvironmentVariable(EnvironmentVariables.RabbitMQHostName)
            };

            services.AddRabbitMQ(rabbitMqConfiguration, () => _serviceProvider);

            services.AddMessageQueueingServices();
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