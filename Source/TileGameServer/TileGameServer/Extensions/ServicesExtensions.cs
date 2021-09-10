using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TileGameServer.Constants;
using TileGameServer.Domain.Models.Configurations;

namespace TileGameServer.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddSingletonSessionCapacityConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            var sessionCapacityConfiguration = configuration
                .GetSection(AppSettings.SessionCapacityConfiguration)
                .Get<SessionCapacityConfiguration>();

            return services.AddSingleton(sessionCapacityConfiguration);
        }
    }
}