using System.Collections.Generic;
using System.Security.Claims;

namespace TileGameServer.Infrastructure.Generators
{
    public interface IJwtGenerator
    {
        string GenerateToken(IEnumerable<Claim> claims);
    }
}