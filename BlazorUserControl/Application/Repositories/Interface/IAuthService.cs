namespace BlazorUserControl.Application.Repositories.Interface;

public interface IAuthService
{
    Task<IUserLogin_Login?> LoginAsync(string email, string password, CancellationToken cancellationToken = default);
    Task LogOutAsync();

    Task<IReadOnlyList<IGetAllUsers_AllUsers_Nodes>?> GetAllUsers();
}