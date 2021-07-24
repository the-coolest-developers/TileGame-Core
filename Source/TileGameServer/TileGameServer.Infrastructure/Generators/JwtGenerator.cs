using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TileGameServer.Infrastructure.Configurators.JwtConfigurators;

namespace TileGameServer.Infrastructure.Generators
{
    public class JwtGenerator : IJwtGenerator
    {
        private IJwtConfigurator JwtConfigurator { get; }

        public JwtGenerator(IJwtConfigurator jwtConfigurator)
        {
            JwtConfigurator = jwtConfigurator;
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var jwtConfiguration = JwtConfigurator.Configuration;
            var signingCredentials = JwtConfigurator.GetSigningCredentials();

            var currentDateTime = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                jwtConfiguration.Issuer,
                jwtConfiguration.Audience,
                claims,
                currentDateTime,
                currentDateTime.AddDays(jwtConfiguration.TokenLifetime),
                signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}