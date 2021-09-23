using Microsoft.Extensions.DependencyInjection;

namespace TileGameServer.InSession.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMessageQueueingServices<TMessageQueueingService>(
            this IServiceCollection services)
            where TMessageQueueingService : class
        {
            services.AddSingleton<TMessageQueueingService>();

            return services;
        }
    }
}