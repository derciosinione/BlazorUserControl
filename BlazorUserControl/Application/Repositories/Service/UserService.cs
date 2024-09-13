using BlazorUserControl.Application.Repositories.Interface;
using FluentResults;
using StrawberryShake;

namespace BlazorUserControl.Application.Repositories.Service;

public class UserService(IR2YGqlClient client) : IUserService
{
    public async Task<Result<IReadOnlyList<IGetAllUsers_AllUsers_Nodes>?>> GetAllUsers()
    {
        var response = await client.GetAllUsers.ExecuteAsync(null, null);

        if (response.IsSuccessResult())
        {
            var users = response.Data?.AllUsers?.Nodes;
            return Result.Ok(users);
        }

        var errors = response.Errors.Select(x => x.Message).ToList();
        return Result.Fail(errors);
    }
}