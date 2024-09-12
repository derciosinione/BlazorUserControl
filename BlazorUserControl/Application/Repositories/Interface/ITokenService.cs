namespace BlazorUserControl.Application.Repositories.Interface;

public interface ITokenService
{
    Task SetTokenAsync(string token);
    Task<string> GetTokenAsync();
    Task RemoveTokenAsync();
}