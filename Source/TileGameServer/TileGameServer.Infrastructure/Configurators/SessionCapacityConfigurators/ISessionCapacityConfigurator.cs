using TileGameServer.Infrastructure.Models.Configurations;

namespace TileGameServer.Infrastructure.Configurators.SessionCapacityConfigurators
{
    public interface ISessionCapacityConfigurator
    {
        SessionCapacityConfiguration Configuration {get;}
    }
}