using System.Net;
using Application.Modules.ContentManagement.Contracts.Menus.Response;
using Application.Modules.ContentManagement.Repositories.Interface;
using FluentResults;
using Newtonsoft.Json;

namespace Application.Modules.ContentManagement.Repositories.Service;

public class ContentManagementService(IHttpClientFactory httpClientFactory) : IContentManagementService
{
    public async Task<Result<ContextResponse>> GetContextMenuByType(string type,
        string language = Constants.DefaultLanguage, CancellationToken cancellationToken = default)
    {
        try
        {
            var pageNumber = 1;
            var pageSize = 10;
            string? search = null;
            
            var queryString = Helpers.Utils.ToQueryString(new
            {
                language,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Search = search
            });
            
            var url = $"/api/contexts/{type}/menus?{queryString}";

            using var httpClient = httpClientFactory.CreateClient(Constants.RetryHttpClientName);

            var httpResponse = await httpClient.GetAsync(url, cancellationToken);

            var httpResponseContent = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

            if (httpResponse.StatusCode == HttpStatusCode.NotFound) return Result.Fail("Menu not found");

            var response = JsonConvert.DeserializeObject<ContextResponse>(httpResponseContent);

            return response!;
        }
        catch (HttpRequestException ex)
        {
            //TODO: Implement Error Handler Filter
            throw new Exception("Unable to connect Content Management Service");
        }
    }
}