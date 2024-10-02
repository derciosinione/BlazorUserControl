using Application.Repositories.Interface.Authentications;
using Microsoft.JSInterop;

namespace Application.Repositories.Service.Authentications;

public class TokenService(IJSRuntime jsRuntime) : ITokenService
{
    public async Task SetTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        await jsRuntime.InvokeVoidAsync(JsFunctions.SaveToken, cancellationToken, token);
    }

    public async Task<string> GetTokenAsync(CancellationToken cancellationToken = default)
    {
        return await jsRuntime.InvokeAsync<string>(JsFunctions.GetToken, cancellationToken);
    }

    public async Task RemoveTokenAsync(CancellationToken cancellationToken = default)
    {
        await jsRuntime.InvokeVoidAsync(JsFunctions.RemoveToken, cancellationToken);
    }

    public string GetToken()
    {
        return Task.Run(async () => await jsRuntime.InvokeAsync<string>(JsFunctions.GetToken)).Result;
    }
}