using System.Diagnostics.CodeAnalysis;

namespace TileGameServer.InSession.Constants
{
    public static class EnvironmentVariables
    {
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public const string RabbitMQHostName = "CORE_RABBITMQ_HOSTNAME";
    }
}