using Application.Modules.R2Y.Repositories.Interface.Authentications;
using Application.Provider;
using StrawberryShake;

namespace Application.Modules.R2Y.Repositories.Service.Authentications;

public class AuthService(IR2YGqlClient client, ITokenService tokenService, AppAuthStateProvider appAuthStateProvider)
    : IAuthService
{
    public async Task<IUserLogin_Login?> LoginAsync(string email, string password,
        CancellationToken cancellationToken = default)
    {
        var result = await client.UserLogin.ExecuteAsync(email, password, cancellationToken);
        result.EnsureNoErrors();

        var login = result.Data!.Login;

        await tokenService.SetTokenAsync(login.Token, cancellationToken);
        appAuthStateProvider.MarkUserAsAuthenticated(login.Token);

        return login;
    }

    public async Task LogOutAsync(CancellationToken cancellationToken = default)
    {
        await appAuthStateProvider.MarkUserAsLoggedOutAsync(cancellationToken);
    }
}