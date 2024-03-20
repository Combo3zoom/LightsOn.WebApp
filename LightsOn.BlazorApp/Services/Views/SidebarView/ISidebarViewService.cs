namespace LightsOn.BlazorApp.Services.Views.SidebarView;

public interface ISidebarViewService
{
    void NavigateToHome();
    void NavigateToOffers();
    void NavigateToClients();
    void NavigateToSignIn();
    void NavigateToSignOut();
}