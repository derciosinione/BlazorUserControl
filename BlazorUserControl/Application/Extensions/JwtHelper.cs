using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlazorUserControl.Application.Extensions;

public abstract class JwtHelper
{
    public static IEnumerable<Claim> ExtractClaims(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        
        var jwtToken = handler.ReadJwtToken(token);

        var claims = jwtToken.Claims.ToList();

        return claims;
    }
}