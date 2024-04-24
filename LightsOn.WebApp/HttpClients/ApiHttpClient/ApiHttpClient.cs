using System.Text;
using System.Text.Json;
using LightsOn.WebApp.Views.Components.CallButtonContainer;
using RESTFulSense.WebAssembly.Clients;

namespace LightsOn.WebApp.HttpClients.ApiHttpClient;

public class ApiHttpClient : RESTFulApiFactoryClient, IApiHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly string _url;
    public ApiHttpClient(HttpClient httpClient) : base(httpClient)
    {
        _httpClient = httpClient;
        _url = "https://localhost:5001/api";
    }
    
    public async Task<HttpResponseMessage> SubmitCustomerAsync(Customer customer)
    {
        var json = JsonSerializer.Serialize(customer);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        return await _httpClient.PostAsync(_url + "/Customers", content);
    }

    public HttpClient GetHttpClient => _httpClient;
}