using System.Diagnostics.CodeAnalysis;

namespace TileGameServer.Constants
{
    public static class EnvironmentVariables
    {
        public const string DatabaseConnectionString = "CORE_DB_CONNECTION_STRING";
        
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public const string RabbitMQHostName = "CORE_RABBITMQ_HOSTNAME";
    }
}