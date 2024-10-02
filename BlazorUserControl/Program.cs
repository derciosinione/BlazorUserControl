using Application.Extensions;
using BlazorUserControl;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var environment = builder.HostEnvironment.Environment;

builder.Configuration.AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{environment}.json", true, true);

builder.RegisterAppDependencyInjections();

await builder.Build().RunAsync();