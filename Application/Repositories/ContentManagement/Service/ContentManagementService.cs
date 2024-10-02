using System.Net;
using Application.Contracts.Menus;
using Application.Repositories.ContentManagement.Interface;
using FluentResults;
using Newtonsoft.Json;

namespace Application.Repositories.ContentManagement.Service;

public class ContentManagementService(IHttpClientFactory httpClientFactory) : IContentManagementService
{
    public async Task<Result<ContextResponse>> GetContextMenuByType(string type,
        string language = Constants.DefaultLanguage, CancellationToken cancellationToken = default)
    {
        try
        {
            var url = $"/api/contexts/{type}/menus?language={language}";

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