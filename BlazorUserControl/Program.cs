using BlazorUserControl;
using BlazorUserControl.Application.Extensions;
using BlazorUserControl.Application.Provider;
using BlazorUserControl.Application.Repositories.Interface;
using BlazorUserControl.Application.Repositories.Service;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<ITokenService, TokenService>();

builder.Services
    .AddR2YGqlClient()
    .ConfigureHttpClient(GraphQlClient.ConfigureClient);

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AppAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, AppAuthStateProvider>();

await builder.Build().RunAsync();