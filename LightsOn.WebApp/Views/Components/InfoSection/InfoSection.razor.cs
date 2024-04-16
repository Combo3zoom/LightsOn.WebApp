using System.Collections.Immutable;
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
    public InfoSection()
    {
        var phoneNumberItems = ImmutableList.Create(
            items: new[]
            {
                new PhoneNumberMenuItem("+380 67 370 27 37"),
                new PhoneNumberMenuItem("+380 67 672 48 60"),
            });
        
        var streetItems = ImmutableList.Create(
            items: new[]
            {
                new StreetMenuItem("вул. Конюшинна 10"),
            });

        PhoneNumberMenu = new PhoneNumberMenu(phoneNumberItems);
        StreetMenu = new StreetMenu(streetItems);
    }
    
    public PhoneNumberMenu PhoneNumberMenu { get; set; }
    public StreetMenu StreetMenu { get; set; }
}