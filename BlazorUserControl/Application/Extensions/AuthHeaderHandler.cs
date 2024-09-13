using System.Net.Http.Headers;
using BlazorUserControl.Application.Repositories.Interface.Authentications;

namespace BlazorUserControl.Application.Extensions;

public class AuthHeaderHandler(ITokenService tokenService) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await tokenService.GetTokenAsync();

        if (!string.IsNullOrEmpty(token))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken);
    }
}