using LightsOn.BlazorApp.Brokers.Apis;
using LightsOn.BlazorApp.Brokers.Navigations;
using LightsOn.BlazorApp.Extensions;
using LightsOn.BlazorApp.HttpClients.ApiHttpClient;
using LightsOn.BlazorApp.Models.Configurations;
using LightsOn.BlazorApp.Services.Foundations.Clients;
using LightsOn.BlazorApp.Services.Views.ClientViews;
using LightsOn.BlazorApp.Services.Views.SidebarView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using RESTFulSense.Clients;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddEnvironmentVariables()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>();

builder.Services.Configure<ApiConfigurations>(builder.Configuration.GetSection("ApiConfigurations"));
builder.Services
    .AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));
builder.Services
    .AddControllersWithViews()
    .AddMicrosoftIdentityUI();
builder.Services.AddAuthorization(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAssertion(_ => true) // Always succeeds
        .Build();

    options.FallbackPolicy = policy;
});
builder.Services
    .AddRazorPages(options => options.RootDirectory = "/Views/Pages");

builder.Services
    .AddServerSideBlazor()
    .AddMicrosoftIdentityConsentHandler();
builder.Services.AddSyncfusionBlazor();

builder.Services.Configure<OpenIdConnectOptions>(
    OpenIdConnectDefaults.AuthenticationScheme,
    options =>
    {
        options.Events.OnSignedOutCallbackRedirect = async context =>
        {
            context.HttpContext.Response.Redirect(context.Options.SignedOutRedirectUri);
            context.HandleResponse();
        };
    });

builder.Services.AddHttpClient<IApiHttpClient, ApiHttpClient>(
    (provider, client) =>
    {
        var optionsApiConfigurations =
            provider.GetRequiredService<IOptions<ApiConfigurations>>();
        var baseAddress = optionsApiConfigurations.Value.Url;
        client.BaseAddress = new Uri(baseAddress);
    });
builder.Services.AddTransient<IApiBroker, ApiBroker>();
builder.Services.AddTransient<INavigationBroker, NavigationBroker>();
builder.Services.AddClientService();
builder.Services.AddClientViewService();

builder.Services.AddTransient<ISidebarViewService, SidebarViewService>();

var app = builder.Build();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
    builder.Configuration.GetValue<string>("SyncfusionLicenseKey"));

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseForwardedHeaders();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();