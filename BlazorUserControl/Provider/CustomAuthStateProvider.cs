using System.Security.Claims;
using BlazorUserControl.Application.Extensions;
using BlazorUserControl.Application.Repositories.Interface;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorUserControl.Provider;

public class CustomAuthStateProvider(ITokenService tokenService) : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await tokenService.GetTokenAsync();

        ClaimsIdentity identity;

        if (!string.IsNullOrEmpty(token))
        {
            // Validate token (if needed) and set the user's identity
            identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwtAuthType");
        }
        else
        {
            identity = new ClaimsIdentity();
        }

        var user = new ClaimsPrincipal(identity);
        return new AuthenticationState(user);
    }

    public void MarkUserAsAuthenticated(string token)
    {
        var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwtAuthType");
        var user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public void MarkUserAsLoggedOut()
    {
        var identity = new ClaimsIdentity();
        var user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = JwtHelper.ExtractClaims(jwt);
        return claims;
    }
}