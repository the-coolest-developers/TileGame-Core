using Microsoft.IdentityModel.Tokens;
using TileGameServer.Infrastructure.Models.Configurations;

namespace TileGameServer.Infrastructure.Configurators.JwtConfigurators
{
    public interface IJwtConfigurator
    {
        JwtConfiguration Configuration { get; }
        TokenValidationParameters ValidationParameters { get; }
        SigningCredentials GetSigningCredentials();
    }
}