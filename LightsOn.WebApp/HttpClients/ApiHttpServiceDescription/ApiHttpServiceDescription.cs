using System.Text;
using System.Text.Json;
using LightsOn.WebApp.HttpClients.ApiHttpClient;
using LightsOn.WebApp.Models.ServiceDescription;
using LightsOn.WebApp.Views.Components.CallButtonContainer;
using RESTFulSense.WebAssembly.Clients;

namespace LightsOn.WebApp.HttpClients.ApiHttpServiceDescription;

public class ApiHttpServiceDescription : RESTFulApiFactoryClient, IApiHttpServiceDescription
{
    private readonly HttpClient _httpClient;
    private readonly string _url;
    
    public ApiHttpServiceDescription(HttpClient httpClient) : base(httpClient)
    {
        _httpClient = httpClient;
        _url = "https://localhost:5001/api";
    }

    public async Task<List<ServiceDescription>?> GetServiceDescriptionsAsync()
    {
        var response = await _httpClient.GetAsync(_url + "/ServiceDescriptions");
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ServiceDescription>>
                (json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        else
        {
            throw new HttpRequestException($"Failed to fetch service descriptions. Status code: {response.StatusCode}");
        }
    }
}