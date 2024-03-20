using LightsOn.BlazorApp.Services.Foundations.Clients;
using LightsOn.BlazorApp.Services.Views.ClientViews;

namespace LightsOn.BlazorApp.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddClientService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IClientService, ClientService>();
    }
    
    public static void AddClientViewService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IClientViewService, ClientViewService>();
    }
}