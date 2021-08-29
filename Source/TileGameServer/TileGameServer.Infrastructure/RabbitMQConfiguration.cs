using System.Diagnostics.CodeAnalysis;

namespace TileGameServer.Infrastructure
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class RabbitMQConfiguration
    {
        public string HostName { get; set; }
    }
}