using LightsOn.WebApp.Services.Foundations.Clients;
using LightsOn.WebApp.Services.Views.ClientViews;

namespace LightsOn.WebApp.Extensions;

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