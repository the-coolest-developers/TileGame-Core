using System.Reflection;
using TileGameServer.BaseLibrary.DataAccess.EntityConfigurations;
using WebApiBaseLibrary.DataAccess;

namespace TileGameServer.DataAccess
{
    public class ConfigurationAssembly : IEntityConfigurationAssembly
    {
        public Assembly GetEntityConfigurationAssembly() => GetType().Assembly;
    }
}