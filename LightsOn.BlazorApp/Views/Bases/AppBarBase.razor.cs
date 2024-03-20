using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;

namespace LightsOn.BlazorApp.Views.Bases;

public partial class AppBarBase
{
    [Inject]
    public IJSRuntime JSRuntime { get; set; }
    [Inject] 
    public required NavigationManager NavigationManager { get; set; }
    [CascadingParameter]
    public Task<AuthenticationState>? AuthenticationStateTask { get; set; }
    [Inject] public ILogger<AppBarBase> Logger { get; set; }
    public bool IsAuthorized { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (AuthenticationStateTask == null)
        {
            await base.OnInitializedAsync();
            return;
        }
            
            
        var authenticationState = await AuthenticationStateTask;
        IsAuthorized = authenticationState.User.Identity?.IsAuthenticated ?? false;
        
        await base.OnInitializedAsync();
    }

    public async Task OnLoginButtonPressed()
    {
        NavigationManager.NavigateTo("MicrosoftIdentity/Account/SignIn", forceLoad: true);
    }
    
    public async Task OnLogoutButtonPressed()
    {
        NavigationManager.NavigateTo("MicrosoftIdentity/Account/SignOut", true);
    }
    
    public async Task OnMainButtonPressed()
    {
        NavigationManager.NavigateTo("/");
    }
    
    public async Task OnContactsButtonPressed()
    {
        await JSRuntime.InvokeVoidAsync("scrollToElement", "workingHoursMap");
    }
    public async Task OnServicesButtonPressed()
    {
        NavigationManager.NavigateTo("/Services");
    }
}