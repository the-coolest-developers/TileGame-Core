using System.Reflection;
using WebApiBaseLibrary.DataAccess;

namespace TileGameServer.DataAccess
{
    public class ConfigurationAssembly : IEntityConfigurationAssembly
    {
        public Assembly GetEntityConfigurationAssembly() => GetType().Assembly;
    }
}