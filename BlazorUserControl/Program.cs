using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorUserControl;
using BlazorUserControl.Application.Extensions;
using BlazorUserControl.Application.Repositories.Interface;
using BlazorUserControl.Application.Repositories.Service;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<ITokenService, TokenService>();

builder.Services
    .AddR2YGqlClient()
    .ConfigureHttpClient(GraphQlClient.ConfigureClient);

await builder.Build().RunAsync();