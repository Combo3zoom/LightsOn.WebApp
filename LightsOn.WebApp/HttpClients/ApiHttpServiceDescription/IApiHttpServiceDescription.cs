using LightsOn.WebApp.Models.ServiceDescription;
using RESTFulSense.WebAssembly.Clients;

namespace LightsOn.WebApp.HttpClients.ApiHttpServiceDescription;

public interface IApiHttpServiceDescription : IRESTFulApiFactoryClient
{
    Task<List<ServiceDescription>?> GetServiceDescriptionsAsync();
}