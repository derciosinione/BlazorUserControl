using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorUserControl.Application.Extensions;

public static class AuthorizationPolicyExtension
{
    public static void AddAuthorizationPolicy(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddAuthorizationCore();
    }
}