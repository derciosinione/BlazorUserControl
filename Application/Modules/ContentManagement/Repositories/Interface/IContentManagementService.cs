using Application.Modules.ContentManagement.Contracts.Menus.Response;
using FluentResults;

namespace Application.Modules.ContentManagement.Repositories.Interface;

public interface IContentManagementService
{
    Task<Result<ContextResponse>> GetContextMenuByType(string type, string language = Constants.DefaultLanguage,
        CancellationToken cancellationToken = default);
}