using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BlazorUserControl.Application.Extensions;

public abstract class JwtHelper
{
    public static IEnumerable<Claim?>? ExtractClaims(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var claims = jwtToken.Claims.ToList();
        var issuer = claims.FirstOrDefault(c => c.Type == "iss")?.Value;
        var principal = ValidateToken(token, issuer!);
        return principal;
    }
    
    private static IEnumerable<Claim>? ValidateToken(string token, string issuer)
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
            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            return principal.Claims;
        }
        catch (SecurityTokenExpiredException)
        {
            Debug.WriteLine("Token has expired.");
            return null;
        }
        catch (SecurityTokenException ex)
        {
            Debug.WriteLine($"Token validation failed: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
    }
}