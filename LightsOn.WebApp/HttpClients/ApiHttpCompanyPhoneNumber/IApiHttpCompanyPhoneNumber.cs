using LightsOn.WebApp.Models.PhoneNumber;
using RESTFulSense.WebAssembly.Clients;

namespace LightsOn.WebApp.HttpClients.ApiHttpCompanyPhoneNumber;

public interface IApiHttpCompanyPhoneNumber : IRESTFulApiFactoryClient
{
    Task<List<CompanyPhoneNumber>?> GetCompanyPhoneNumbersAsync();
}