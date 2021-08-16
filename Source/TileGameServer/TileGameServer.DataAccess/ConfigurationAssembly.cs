using System.Reflection;
using TileGameServer.BaseLibrary.DataAccess.EntityConfigurations;

namespace TileGameServer.DataAccess
{
    public class ConfigurationAssembly : IConfigurationAssembly
    {
        public Assembly GetConfigurationAssembly() => GetType().Assembly;
    }
}