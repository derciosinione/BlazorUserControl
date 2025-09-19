using Application.Modules.R2Y.Repositories.Interface.Users;
using FluentResults;
using StrawberryShake;

namespace Application.Modules.R2Y.Repositories.Service.Users;

public class UserService(IR2YGqlClient client) : IUserService
{
    public async Task<Result<IReadOnlyList<IGetAllUsers_AllUsers>?>> GetAllUsers(
        CancellationToken cancellationToken = default)
    {
        var response = await client.GetAllUsers.ExecuteAsync( cancellationToken);

        if (response.IsSuccessResult())
        {
            var users = response.Data?.AllUsers;
            return Result.Ok(users);
        }

        var errors = response.Errors.Select(x => x.Message).ToList();
        return Result.Fail(errors);
    }
}