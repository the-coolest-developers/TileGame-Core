using System.Security.Claims;

namespace TileGameServer.Extensions
{
    public class ApplicationClaimTypes
    {
        public const string AccountId = "AccountId";
        public const string SessionId = "SessionId";

        public const string UserRole = ClaimTypes.Role;
    }
}