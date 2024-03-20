using Microsoft.AspNetCore.Components;

namespace LightsOn.BlazorApp.Views.Components.CardContainer;

public partial class CardContainer : ComponentBase
{
    [Inject] 
    public required NavigationManager NavigationManager { get; set; }
    
    private Task OnButtonPressed()
    {
        NavigationManager.NavigateTo("/Services");
        return Task.CompletedTask;
    }
}