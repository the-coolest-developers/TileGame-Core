using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TileGameServer.InSession.Attributes;
using WebApiBaseLibrary.Infrastructure.MessageQueueing;

namespace TileGameServer.InSession.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMessageQueueingHostingServices<TMessageQueueingHostedService>(
            this IServiceCollection services,
            IMessageQueueConnectionFactory messageQueueConnectionFactory)
            where TMessageQueueingHostedService : class, IHostedService
        {
            services.AddHostedService<TMessageQueueingHostedService>();

            var assembly = Assembly.GetAssembly(typeof(Startup));

            var mqHostedService = assembly?.GetType(nameof(TMessageQueueingHostedService));
            if (mqHostedService != null)
            {
                var methods = mqHostedService.GetMethods()
                    .Where(m => m.GetCustomAttributes(typeof(QueueActionAttribute), false).Any())
                    .ToArray();
            }

            return services;
        }
    }
}