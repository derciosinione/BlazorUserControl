namespace BlazorUserControl.Application.Repositories.Interface.Authentications;

public interface ITokenService
{
    Task SetTokenAsync(string token, CancellationToken cancellationToken = default);
    Task<string> GetTokenAsync(CancellationToken cancellationToken = default);
    Task RemoveTokenAsync(CancellationToken cancellationToken = default);
}