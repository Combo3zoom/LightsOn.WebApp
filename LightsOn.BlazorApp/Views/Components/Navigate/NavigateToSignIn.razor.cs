using Microsoft.AspNetCore.Components;

namespace LightsOn.BlazorApp.Views.Components.Navigate;

public partial class NavigateToSignIn : ComponentBase
{
    [Inject] public NavigationManager NavigationManager { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        
        NavigationManager.NavigateTo("MicrosoftIdentity/Account/SignIn", forceLoad: true);
    }
}