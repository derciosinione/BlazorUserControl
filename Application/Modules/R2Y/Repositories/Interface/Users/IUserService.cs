using FluentResults;

namespace Application.Modules.R2Y.Repositories.Interface.Users;

public interface IUserService
{
    Task<Result<IReadOnlyList<IGetAllUsers_AllUsers>?>> GetAllUsers(
        CancellationToken cancellationToken = default);
}