using Microsoft.IdentityModel.Tokens;
using TileGameServer.Infrastructure.Models.Configurations;

namespace TileGameServer.Infrastructure.Configurators
{
    public interface IJwtConfigurator
    {
        JwtConfiguration Configuration { get; }
        TokenValidationParameters ValidationParameters { get; }
        SigningCredentials GetSigningCredentials();
    }
}