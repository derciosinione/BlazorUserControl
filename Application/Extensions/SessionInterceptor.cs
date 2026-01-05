using StrawberryShake.Transport.WebSockets;
using Application.Modules.R2Y.Repositories.Interface.Authentications;

namespace Application.Extensions;

public class SessionInterceptor(ITokenService tokenService) : ISocketConnectionInterceptor
{
    public async ValueTask<object?> CreateConnectionInitPayload(ISocketProtocol protocol, CancellationToken cancellationToken)
    {
        var token = await tokenService.GetTokenAsync();
        if (!string.IsNullOrEmpty(token))
        {
            return new Dictionary<string, object>
            {
                { "Authorization", $"Bearer {token}" }
            };
        }
        return null;
    }

    public ValueTask OnConnectionAcknowledgedAsync(ISocketProtocol protocol, object? payload, CancellationToken cancellationToken)
    {
        return ValueTask.CompletedTask;
    }

    public ValueTask OnErrorAsync(ISocketProtocol protocol, Exception exception, CancellationToken cancellationToken)
    {
        return ValueTask.CompletedTask;
    }

    public ValueTask OnConnectionClosedAsync(ISocketProtocol protocol, CancellationToken cancellationToken)
    {
        return ValueTask.CompletedTask;
    }

    public void OnConnectionOpened(ISocketClient client)
    {
    }

    public void OnConnectionClosed(ISocketClient client)
    {
    }
}
