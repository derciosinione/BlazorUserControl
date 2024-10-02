using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class GraphQlExtension
{
    public static void AddGraphQlClient(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddTransient<AuthHeaderHandler>();

        var apiUrl = builder.Configuration["GraphQlApi:BaseUrl"]!;

        builder.Services.AddHttpClient(R2YGqlClient.ClientName,
                client => client.BaseAddress = new Uri(apiUrl))
            .AddHttpMessageHandler<AuthHeaderHandler>();

        builder.Services.AddR2YGqlClient();
    }
}