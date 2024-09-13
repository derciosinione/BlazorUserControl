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

        //TODO:: Invalidar a sessao do user caso token for invalido
        var identity = !string.IsNullOrEmpty(token)
            ? new ClaimsIdentity(JwtHelper.ExtractClaims(token)!, Constants.JwtAuthType)
            : new ClaimsIdentity();

        var user = new ClaimsPrincipal(identity);
        return new AuthenticationState(user);
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

    private static IEnumerable<Claim?>? ParseClaimsFromJwt(string jwt)
    {
        return JwtHelper.ExtractClaims(jwt);
    }

    private static ClaimsPrincipal GetUser(ClaimsIdentity identity)
    {
        return new ClaimsPrincipal(identity);
    }
}