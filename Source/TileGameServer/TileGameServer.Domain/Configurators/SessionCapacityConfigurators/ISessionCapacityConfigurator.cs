using TileGameServer.Domain.Models.Configurations;

namespace TileGameServer.Domain.Configurators.SessionCapacityConfigurators
{
    public interface ISessionCapacityConfigurator
    {
        SessionCapacityConfiguration Configuration {get;}
    }
}