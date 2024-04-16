using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using LightsOn.WebApp;
using LightsOn.WebApp.Brokers.Apis;
using LightsOn.WebApp.Brokers.Navigations;
using LightsOn.WebApp.Extensions;
using LightsOn.WebApp.HttpClients.ApiHttpClient;
using LightsOn.WebApp.Models.Configurations;
using LightsOn.WebApp.Services.Views.SidebarView;
using Microsoft.Extensions.Options;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.Configure<ApiConfigurations>(builder.Configuration.GetSection("ApiConfigurations"));


builder.Services.AddScoped(sp 
    => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
    var azureAdB2C = builder.Configuration.GetSection("AzureAdB2C");
    options.ProviderOptions.Authentication.Authority = $"{azureAdB2C["Instance"]}/{azureAdB2C["Domain"]}/{azureAdB2C["SignUpSignInPolicyId"]}";
    options.ProviderOptions.Authentication.ClientId = azureAdB2C["ClientId"];
    options.ProviderOptions.Authentication.PostLogoutRedirectUri = azureAdB2C["SignedOutCallbackPath"];
    options.ProviderOptions.DefaultAccessTokenScopes.Add($"{azureAdB2C["Instance"]}/.default");
    options.ProviderOptions.LoginMode = "redirect";
});


builder.Services.AddHttpClient<IApiHttpClient, ApiHttpClient>(
    (provider, client) =>
    {
        var optionsApiConfigurations = provider.GetRequiredService<IOptions<ApiConfigurations>>();
        var baseAddress = optionsApiConfigurations.Value.Url;
        client.BaseAddress = new Uri(baseAddress);  
    });

builder.Services.AddTransient<IApiBroker, ApiBroker>();
builder.Services.AddTransient<INavigationBroker, NavigationBroker>();
builder.Services.AddClientService();
builder.Services.AddClientViewService();
builder.Services.AddTransient<ISidebarViewService, SidebarViewService>();

builder.Services.AddSyncfusionBlazor();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
    builder.Configuration.GetValue<string>("SyncfusionLicenseKey"));

var host = builder.Build();

var httpClient = host.Services.GetRequiredService<HttpClient>();
var appSettings = await httpClient.GetFromJsonAsync<LocalConfigurations>("appsettings.json");

await host.RunAsync();