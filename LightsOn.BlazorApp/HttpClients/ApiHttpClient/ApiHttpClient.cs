using RESTFulSense.Clients;

namespace LightsOn.BlazorApp.HttpClients.ApiHttpClient;

public class ApiHttpClient : RESTFulApiFactoryClient, IApiHttpClient
{
    public ApiHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }
}