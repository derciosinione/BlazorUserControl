using BlazorUserControl.Application.Repositories.Interface;
using Microsoft.JSInterop;

namespace BlazorUserControl.Application.Repositories.Service;

public class TokenService : ITokenService
{
    private readonly IJSRuntime _jsRuntime;

    public TokenService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task SetTokenAsync(string token)
    {
        await _jsRuntime.InvokeVoidAsync(JsFunctions.SaveToken, token);
    }

    public async Task<string> GetTokenAsync()
    {
        return await _jsRuntime.InvokeAsync<string>(JsFunctions.GetToken);
    }

    public string GetToken()
    {
        return Task.Run(async () => await _jsRuntime.InvokeAsync<string>(JsFunctions.GetToken)).Result;
    }

    public async Task RemoveTokenAsync()
    {
        await _jsRuntime.InvokeVoidAsync(JsFunctions.RemoveToken);
    }
}