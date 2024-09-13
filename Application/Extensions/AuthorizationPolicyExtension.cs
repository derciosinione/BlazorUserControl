using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class AuthorizationPolicyExtension
{
    public static void AddAuthorizationPolicy(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddAuthorizationCore();
    }
}