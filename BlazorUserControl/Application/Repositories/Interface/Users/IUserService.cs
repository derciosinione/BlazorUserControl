using FluentResults;

namespace BlazorUserControl.Application.Repositories.Interface.Users;

public interface IUserService
{
    Task<Result<IReadOnlyList<IGetAllUsers_AllUsers_Nodes>?>>
        GetAllUsers(CancellationToken cancellationToken = default);
}