using System.Collections.Immutable;
using LightsOn.WebApp.HttpClients.ApiHttpClient;
using Microsoft.AspNetCore.Components;

namespace LightsOn.WebApp.Views.Components.Service;

public sealed record ServiceMenu(
    ImmutableList<ServiceItem> ServiceItems);
public sealed record ServiceItem(string HeaderText, string MainText, string LowerPriceLimit);
public partial class Service
{
    protected override async Task OnInitializedAsync()
    {
        var serviceDescriptions =  await ApiServiceDescription?.GetServiceDescriptionsAsync();
        if (serviceDescriptions != null)
        {
            var servicesItems = serviceDescriptions
                .Select(pn => new ServiceItem(pn.HeaderText, pn.MainText, pn.LowerPriceLimit))
                .ToImmutableList();
            ServiceMenu = new ServiceMenu(servicesItems);
        }
    }
    
    public ServiceMenu ServiceMenu { get; set; }
    
    [Inject]
    private IApiHttpClient ApiServiceDescription { get; set; }
}