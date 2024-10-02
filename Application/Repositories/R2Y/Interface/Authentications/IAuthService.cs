namespace Application.Repositories.Interface.Authentications;

public interface IAuthService
{
    Task<IUserLogin_Login?> LoginAsync(string email, string password, CancellationToken cancellationToken = default);
    Task LogOutAsync(CancellationToken cancellationToken = default);
}