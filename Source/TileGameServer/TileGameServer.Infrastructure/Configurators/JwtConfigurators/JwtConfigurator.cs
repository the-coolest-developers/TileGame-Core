using System.Text;
using Microsoft.IdentityModel.Tokens;
using TileGameServer.Infrastructure.Models.Configurations;

namespace TileGameServer.Infrastructure.Configurators.JwtConfigurators
{
    public class JwtConfigurator : IJwtConfigurator
    {
        public JwtConfiguration Configuration { get; }

        public JwtConfigurator(JwtConfiguration jwtConfiguration)
        {
            Configuration = jwtConfiguration;
        }

        public TokenValidationParameters ValidationParameters => new()
        {
            ValidateIssuer = true,
            ValidIssuer = Configuration.Issuer,
            ValidateAudience = true,
            ValidAudience = Configuration.Audience,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            IssuerSigningKey = GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true
        };

        public SigningCredentials GetSigningCredentials() =>
            new(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256Signature);

        private SymmetricSecurityKey GetSymmetricSecurityKey() => new(GetByteKey());
        private byte[] GetByteKey() => Encoding.UTF8.GetBytes(Configuration.SecretKey);
    }
}