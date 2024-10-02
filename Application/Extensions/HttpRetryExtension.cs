using System.Net;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace Application.Extensions;

public static class HttpRetryExtension
{
    public static void AddAHttpRetryExtensionService(this WebAssemblyHostBuilder builder, int retryCount = 3)
    {
        var apiUrl = builder.Configuration["ContentManagementApi:BaseUrl"]!;

        builder.Services.AddHttpClient(Constants.RetryHttpClientName,
                client => { client.BaseAddress = new Uri(apiUrl); })
            .AddPolicyHandler(GetRetryPolicy(retryCount));
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(int retryCount)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError() // Handle 5xx and network-related errors
            .OrResult(response => response.StatusCode == HttpStatusCode.NotFound) // Handle NotFound
            .WaitAndRetryAsync(retryCount, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), // Exponential backoff
                (outcome, timeSpan, attempt, context) =>
                {
                    // Optionally log the retry attempt
                    Console.WriteLine(
                        $"Retry {attempt} encountered an error: {outcome.Exception?.Message}. Waiting {timeSpan} before next retry.");
                });
    }
}