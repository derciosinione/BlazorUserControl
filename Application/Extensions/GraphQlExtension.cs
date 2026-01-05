using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using StrawberryShake;

namespace Application.Extensions;

public static class GraphQlExtension
{
    public static void AddGraphQlClient(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddTransient<AuthHeaderHandler>();

        var apiUrl = builder.Configuration["GraphQlApi:BaseUrl"]!;

        builder.Services.AddTransient<SessionInterceptor>();

        builder.Services.AddR2YGqlClient()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri(apiUrl),
                builder => builder.AddHttpMessageHandler<AuthHeaderHandler>())
            .ConfigureWebSocketClient(client => client.Uri = new Uri(apiUrl.Replace("http", "ws")),
                builder => builder.ConfigureConnectionInterceptor<SessionInterceptor>());
    }
}