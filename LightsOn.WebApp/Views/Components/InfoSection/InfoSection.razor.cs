using System.Collections.Immutable;
using LightsOn.WebApp.HttpClients.ApiHttpClient;
using LightsOn.WebApp.HttpClients.ApiHttpClient;
using Microsoft.AspNetCore.Components;

namespace LightsOn.WebApp.Views.Components.InfoSection;

public sealed record PhoneNumberMenu(
    ImmutableList<PhoneNumberMenuItem> PhoneNumberItems);
public sealed record PhoneNumberMenuItem(string PhoneNumber);

public sealed record StreetMenu(
    ImmutableList<StreetMenuItem> StreetMenuItems);
public sealed record StreetMenuItem(string Street);
public partial class InfoSection : ComponentBase
{
    protected override async Task OnInitializedAsync()
    {
        var phoneNumbers =  await ApiHttpCompanyPhoneNumber?.GetCompanyPhoneNumbersAsync();
        if (phoneNumbers != null)
        {
            var phoneNumberItems = phoneNumbers
                .Select(pn => new PhoneNumberMenuItem(FormatPhoneNumber(pn.PhoneNumber)))
                .ToImmutableList();
            PhoneNumberMenu = new PhoneNumberMenu(phoneNumberItems);
        }
        
        var streetItems = ImmutableList.Create(
            items: new[]
            {
                new StreetMenuItem("вул. Конюшинна 10"),
            });
        
        StreetMenu = new StreetMenu(streetItems);
    }
    
    private string FormatPhoneNumber(string rawNumber)
    {
        if (rawNumber.StartsWith("+380"))
            return $"{rawNumber.Substring(0, 4)} {rawNumber.Substring(4, 2)}" +
                   $" {rawNumber.Substring(6, 3)} {rawNumber.Substring(9, 2)}" +
                   $" {rawNumber.Substring(11, 2)}";
        return rawNumber;
    }
    
    public PhoneNumberMenu PhoneNumberMenu { get; set; }
    public StreetMenu StreetMenu { get; set; }
    
    [Inject]
    private IApiHttpClient ApiHttpCompanyPhoneNumber { get; set; }

}