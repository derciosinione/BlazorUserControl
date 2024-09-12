﻿using BlazorUserControl.Application.Repositories.Interface;
using BlazorUserControl.Provider;
using Microsoft.AspNetCore.Components.Authorization;
using StrawberryShake;

namespace BlazorUserControl.Application.Repositories.Service;

public class AuthService(IR2YGqlClient client, ITokenService tokenService, CustomAuthStateProvider customAuthStateProvider) : IAuthService
{
    public async Task<IUserLogin_Login?> LoginAsync(string email, string password,
        CancellationToken cancellationToken = default)
    {
        var result = await client.UserLogin.ExecuteAsync(email, password, cancellationToken);
        result.EnsureNoErrors();

        var login = result.Data!.Login;

        await tokenService.SetTokenAsync(login.Token);
        
        customAuthStateProvider.MarkUserAsAuthenticated(login.Token);
        
        return login;
    }

    public async Task<IReadOnlyList<IGetAllUsers_AllUsers_Nodes>?> GetAllUsers()
    {
        var result = await client.GetAllUsers.ExecuteAsync(null, null);
        result.EnsureNoErrors();
        return result.Data!.AllUsers!.Nodes;
    }
}