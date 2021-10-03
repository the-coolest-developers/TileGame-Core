using System.Diagnostics.CodeAnalysis;

namespace TileGameServer.InSession.Constants
{
    public static class EnvironmentVariables
    {
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public const string RabbitMQHostName = "CORE_RABBITMQ_HOSTNAME";

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public const string RabbitMQPort = "CORE_RABBITMQ_PORT";

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public const string RabbitMQVirtualHost = "CORE_RABBITMQ_VIRTUALHOST";

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public const string RabbitMQUserName = "CORE_RABBITMQ_USERNAME";

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public const string RabbitMQPassword = "CORE_RABBITMQ_PASSWORD";
    }
}