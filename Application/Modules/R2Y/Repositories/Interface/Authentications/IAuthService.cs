namespace Application.Modules.R2Y.Repositories.Interface.Authentications;

public interface IAuthService
{
    Task<IUserLogin_Login?> LoginAsync(string email, string password, CancellationToken cancellationToken = default);
    Task LogOutAsync(CancellationToken cancellationToken = default);
}