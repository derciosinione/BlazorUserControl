using FluentResults;

namespace BlazorUserControl.Application.Repositories.Interface;

public interface IUserService
{
    Task<Result<IReadOnlyList<IGetAllUsers_AllUsers_Nodes>?>> GetAllUsers();
}