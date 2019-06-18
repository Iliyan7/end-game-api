using System.Collections.Generic;
using System.Security.Claims;

namespace EndGame.Api.TokenProviders.Contracts
{
    public interface ITokenProvider
    {
        string GenerateToken(IEnumerable<Claim> claims);
    }
}