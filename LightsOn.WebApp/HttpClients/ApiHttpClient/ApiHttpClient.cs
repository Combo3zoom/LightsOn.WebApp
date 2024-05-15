using System.Text;
using System.Text.Json;
using LightsOn.WebApp.Models.PhoneNumber;
using LightsOn.WebApp.Models.ServiceDescription;
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
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }
    
    public async Task<HttpResponseMessage> SubmitCustomerAsync(Customer customer)
    {
        var json = JsonSerializer.Serialize(customer);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        return await _httpClient.PostAsync(_url + "/Customers", content);
    }
    
    public async Task<List<CompanyPhoneNumber>?> GetCompanyPhoneNumbersAsync()
    {
        var response = await _httpClient.GetAsync(_url + "/CompanyPhoneNumbers");
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<CompanyPhoneNumber>>
                (json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        else
        {
            throw new HttpRequestException($"Failed to fetch service descriptions. Status code: {response.StatusCode}");
        }
        
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
    public HttpClient GetHttpClient => _httpClient;
}