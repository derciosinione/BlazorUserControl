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

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<ITokenService, TokenService>();

builder.Services
    .AddR2YGqlClient()
    .ConfigureHttpClient(GraphQlClient.ConfigureClient);

builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Perfil", "ADMIN"));
    options.AddPolicy("MemberOnly", policy => policy.RequireClaim("Perfil", "MEM"));
    options.AddPolicy("ManagerOnly", policy => policy.RequireClaim("Perfil", "MR"));
    options.AddPolicy("DNOnly", policy => policy.RequireClaim("Perfil", "DN"));
});

builder.Services.AddScoped<AppAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, AppAuthStateProvider>();

await builder.Build().RunAsync();