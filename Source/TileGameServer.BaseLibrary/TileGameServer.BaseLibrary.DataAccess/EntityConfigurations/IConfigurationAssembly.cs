using System.Reflection;

namespace TileGameServer.BaseLibrary.DataAccess.EntityConfigurations
{
    public interface IConfigurationAssembly
    {
        Assembly GetConfigurationAssembly();
    }
}