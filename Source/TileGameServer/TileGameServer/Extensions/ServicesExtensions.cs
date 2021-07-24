using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TileGameServer.Constants;
using TileGameServer.Infrastructure.Models.Configurations;
using WebApiBaseLibrary.Authorization.Generators;

namespace TileGameServer.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddJwt(this IServiceCollection services)
        {
            return services.AddScoped<IJwtGenerator, JwtGenerator>();
        }

        public static IServiceCollection AddSingletonSessionCapacityConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            var sessionCapacityConfiguration = configuration.GetSection(TileGameAppSettings.SessionCapacityConfiguration)
                .Get<SessionCapacityConfiguration>();

            return services.AddSingleton(sessionCapacityConfiguration);
        }
    }
}