using LightsOn.WebApp.Views.Components.CallButtonContainer;
using RESTFulSense.WebAssembly.Clients;

namespace LightsOn.WebApp.HttpClients.ApiHttpClient;

public interface IApiHttpClient : IRESTFulApiFactoryClient
{
    Task<HttpResponseMessage> SubmitCustomerAsync(Customer customer);
}