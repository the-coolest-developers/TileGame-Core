using TileGameServer.Infrastructure.Models.Configurations;

namespace TileGameServer.Infrastructure.Configurators.SessionCapacityConfigurators
{
    public class SessionCapacityConfigurator : ISessionCapacityConfigurator
    {
        public SessionCapacityConfiguration Configuration { get; }

        public SessionCapacityConfigurator(SessionCapacityConfiguration sessionCapacityConfiguration)
        {
            Configuration = sessionCapacityConfiguration;
        }
    }
}