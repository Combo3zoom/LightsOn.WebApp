using System.Text;
using System.Text.Json;
using LightsOn.WebApp.Models.PhoneNumber;
using RESTFulSense.WebAssembly.Clients;

namespace LightsOn.WebApp.HttpClients.ApiHttpCompanyPhoneNumber;

public class ApiHttpCompanyPhoneNumber : RESTFulApiFactoryClient, IApiHttpCompanyPhoneNumber
{
    private readonly HttpClient _httpClient;
    private readonly string _url;
    
    public ApiHttpCompanyPhoneNumber(HttpClient httpClient) : base(httpClient)
    {
        _httpClient = httpClient;
        _url = "https://localhost:5001/api";
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
}