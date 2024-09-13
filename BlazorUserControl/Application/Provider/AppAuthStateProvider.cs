using System.Security.Claims;
using BlazorUserControl.Application.Extensions;
using BlazorUserControl.Application.Repositories.Interface;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorUserControl.Application.Provider;

public class AppAuthStateProvider(ITokenService tokenService) : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await tokenService.GetTokenAsync();

        var claims = JwtHelper.ExtractClaims(token);

        var identity = new ClaimsIdentity();

        if (claims.Count != 0)
        {
            identity = new ClaimsIdentity(claims, Constants.JwtAuthType);
            return new AuthenticationState(GetUser(identity));
        }

        var authenticationState = new AuthenticationState(GetUser(identity));
        NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
        return authenticationState;
    }

    public void MarkUserAsAuthenticated(string token)
    {
        var identity = new ClaimsIdentity(JwtHelper.ExtractClaims(token)!, Constants.JwtAuthType);
        var claimsPrincipal = GetUser(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    public void MarkUserAsLoggedOut()
    {
        var identity = new ClaimsIdentity();
        var claimsPrincipal = GetUser(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    public async Task<bool> IsAuthenticated()
    {
        var authState = await GetAuthenticationStateAsync();
        return authState.User.Identity!.IsAuthenticated;
    }

    private static ClaimsPrincipal GetUser(ClaimsIdentity identity)
    {
        return new ClaimsPrincipal(identity);
    }
}