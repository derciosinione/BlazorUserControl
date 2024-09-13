using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BlazorUserControl.Application.Extensions;

public static class JwtHelper
{
    public static List<Claim> ExtractClaims(string token)
    {
        if (string.IsNullOrWhiteSpace(token)) return [];

        var handler = new JwtSecurityTokenHandler();

        try
        {
            var jwtToken = handler.ReadJwtToken(token);
            var issuer = jwtToken.Issuer;

            var principal = ValidateToken(token, issuer);
            return principal?.Claims.ToList() ?? [];
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Invalid JWT format: {ex.Message}");
            return [];
        }
    }

    private static ClaimsPrincipal? ValidateToken(string token, string issuer)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.SecretKey))
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
            return principal;
        }
        catch (SecurityTokenExpiredException)
        {
            Console.WriteLine("Token has expired.");
            return null;
        }
        catch (SecurityTokenException ex)
        {
            Console.WriteLine($"Token validation failed: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error during token validation: {ex.Message}");
            return null;
        }
    }
}
