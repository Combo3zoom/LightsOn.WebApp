using LightsOn.WebApp.Models.PhoneNumber;
using LightsOn.WebApp.Models.ServiceDescription;
using LightsOn.WebApp.Views.Components.CallButtonContainer;
using RESTFulSense.WebAssembly.Clients;

namespace LightsOn.WebApp.HttpClients.ApiHttpClient;

public interface IApiHttpClient : IRESTFulApiFactoryClient
{
    Task<HttpResponseMessage> SubmitCustomerAsync(Customer customer);
    Task<List<CompanyPhoneNumber>?> GetCompanyPhoneNumbersAsync();
    Task<List<ServiceDescription>?> GetServiceDescriptionsAsync();
}