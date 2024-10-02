using Application.Contracts.Menus;
using FluentResults;

namespace Application.Repositories.ContentManagement.Interface;

public interface IContentManagementService
{
    Task<Result<ContextResponse>> GetContextMenuByType(string type, string language=Constants.DefaultLanguage, CancellationToken cancellationToken = default); 
}