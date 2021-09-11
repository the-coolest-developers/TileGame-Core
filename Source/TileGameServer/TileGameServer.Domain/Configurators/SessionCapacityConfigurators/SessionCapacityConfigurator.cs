using TileGameServer.Domain.Models.Configurations;

namespace TileGameServer.Domain.Configurators.SessionCapacityConfigurators
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