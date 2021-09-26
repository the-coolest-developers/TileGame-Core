﻿using Microsoft.Extensions.DependencyInjection;
using TileGameServer.InSession.Attributes;
using TileGameServer.Reflection;

namespace TileGameServer.InSession.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMessageQueueingServices(this IServiceCollection services)
        {
            var messageQueueServices = ReflectionHelper.GetAllTypesWithAttribute<MessageQueueServiceAttribute>();

            foreach (var mqService in messageQueueServices)
            {
                services.AddScoped(mqService);
            }

            return services;
        }
    }
}