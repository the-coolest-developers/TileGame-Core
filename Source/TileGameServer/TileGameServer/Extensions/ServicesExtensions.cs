using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TileGameServer.Constants;
using TileGameServer.Infrastructure.Generators;
using TileGameServer.Infrastructure.Models.Configurations;

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
            var sessionCapacityConfiguration = configuration.GetSection(SettingNames.SessionCapacityConfiguration)
                .Get<SessionCapacityConfiguration>();

            return services.AddSingleton(sessionCapacityConfiguration);
        }
    }
}