using BlazorUserControl.Application.Provider;
using BlazorUserControl.Application.Repositories.Interface.Authentications;
using BlazorUserControl.Application.Repositories.Interface.Users;
using BlazorUserControl.Application.Repositories.Service.Authentications;
using BlazorUserControl.Application.Repositories.Service.Users;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorUserControl.Application.Extensions;

public static class AppInjections
{
    public static void RegisterAppDependencyInjections(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddSingleton<ITokenService, TokenService>();
        builder.Services.AddSingleton<IUserService, UserService>();

        builder.Services.AddScoped<AppAuthStateProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider, AppAuthStateProvider>();

        builder.AddGraphQlClient();
        builder.AddAuthorizationPolicy();
    }
}