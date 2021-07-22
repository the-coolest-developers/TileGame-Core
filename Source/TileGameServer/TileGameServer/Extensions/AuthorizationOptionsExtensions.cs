using Microsoft.AspNetCore.Authorization;
using TileGameServer.Constants;
using WebApiBaseLibrary.Authorization.Enums;

namespace TileGameServer.Extensions
{
    public static class AuthorizationOptionsExtensions
    {
        public static void AddRequireAdministratorRolePolicy(this AuthorizationOptions options)
        {
            options.AddPolicy(
                AuthorizationPolicies.RequireAdministratorRole,
                policy => policy.RequireUserRole(UserRole.Admin));
        }
    }
}