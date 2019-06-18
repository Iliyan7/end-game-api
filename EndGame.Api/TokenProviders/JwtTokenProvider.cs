using EndGame.Api.TokenProviders.Contracts;
using EndGame.Shared.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace EndGame.Api.TokenProviders
{
    public class JwtTokenProvider : ITokenProvider
    {
        private readonly TokenProviderOptions _tokenProviderAccessor;

        public JwtTokenProvider(IOptionsMonitor<TokenProviderOptions> tokenProviderAccessor)
        {
            _tokenProviderAccessor = tokenProviderAccessor.CurrentValue;
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenProviderAccessor.IssuerSigningKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: _tokenProviderAccessor.Issuer,
                audience: _tokenProviderAccessor.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(_tokenProviderAccessor.Expires),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return tokenString;
        }
    }
}
