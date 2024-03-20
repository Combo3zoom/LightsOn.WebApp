﻿namespace LightsOn.BlazorApp.Brokers.Navigations;

public interface INavigationBroker
{
    void NavigateToHome();
    void NavigateToOffers();
    void NavigateToClients();
    void NavigateToSignIn();
    void NavigateToSignOut();
}