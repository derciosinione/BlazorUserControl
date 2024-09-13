﻿using System.Security.Claims;
using BlazorUserControl.Application.Extensions;
using BlazorUserControl.Application.Repositories.Interface.Authentications;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorUserControl.Application.Provider;

public class AppAuthStateProvider(ITokenService tokenService) : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await tokenService.GetTokenAsync();
        var claims = JwtHelper.ExtractClaims(token);

        var identity = claims.Count > 0
            ? new ClaimsIdentity(claims, Constants.JwtAuthType)
            : new ClaimsIdentity();

        var authenticationState = new AuthenticationState(GetUser(identity));
        NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));

        return authenticationState;
    }

    public void MarkUserAsAuthenticated(string token)
    {
        var claims = JwtHelper.ExtractClaims(token);
        var identity = new ClaimsIdentity(claims, Constants.JwtAuthType);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(GetUser(identity))));
    }

    public async Task MarkUserAsLoggedOutAsync(CancellationToken cancellationToken = default)
    {
        await tokenService.RemoveTokenAsync(cancellationToken);

        var identity = new ClaimsIdentity();
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(GetUser(identity))));
    }

    public async Task<bool> IsAuthenticated()
    {
        var authState = await GetAuthenticationStateAsync();
        return authState.User.Identity?.IsAuthenticated ?? false;
    }

    private static ClaimsPrincipal GetUser(ClaimsIdentity identity)
    {
        return new ClaimsPrincipal(identity);
    }
}