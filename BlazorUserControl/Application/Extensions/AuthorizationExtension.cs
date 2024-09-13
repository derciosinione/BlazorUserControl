using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorUserControl.Application.Extensions;

public static class AuthorizationExtension
{
    public static void AddAppAuthorization(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddAuthorizationCore();
    }
}