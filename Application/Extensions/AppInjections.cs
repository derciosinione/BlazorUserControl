using Application.Modules.ContentManagement.Repositories.Interface;
using Application.Modules.ContentManagement.Repositories.Service;
using Application.Modules.R2Y.Repositories.Interface.Authentications;
using Application.Modules.R2Y.Repositories.Interface.Users;
using Application.Modules.R2Y.Repositories.Service.Authentications;
using Application.Modules.R2Y.Repositories.Service.Users;
using Application.Modules.R2Y.Repositories.Interface.Chat;
using Application.Modules.R2Y.Repositories.Service.Chat;
using Application.Provider;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class AppInjections
{
    public static void RegisterAppDependencyInjections(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddSingleton<ITokenService, TokenService>();
        builder.Services.AddSingleton<IUserService, UserService>();
        builder.Services.AddSingleton<IContentManagementService, ContentManagementService>();
        builder.Services.AddScoped<IChatService, ChatService>();

        builder.Services.AddScoped<AppAuthStateProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider, AppAuthStateProvider>();

        builder.AddAHttpRetryExtensionService();
        builder.AddGraphQlClient();
        builder.AddAuthorizationPolicy();
    }
}