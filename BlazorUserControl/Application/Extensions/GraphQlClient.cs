using System.Net.Http.Headers;
using BlazorUserControl.Application.Repositories.Interface;

namespace BlazorUserControl.Application.Extensions;

public static class GraphQlClient
{
    public static async void ConfigureClient(IServiceProvider sp, HttpClient client)
    {
        client.BaseAddress = new Uri("https://localhost:8081/graphql");

        var tokenService = sp.GetRequiredService<ITokenService>();
        var token = await tokenService.GetTokenAsync();

        if (!string.IsNullOrEmpty(token))
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}