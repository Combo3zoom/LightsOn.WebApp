using Bunit;
using LightsOn.WebApp.Services.Views.ClientViews;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Syncfusion.Blazor;

namespace LightsOn.BlazorApp.Tests.Unit.Views.Components.Client;

[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
public partial class ClientGridComponentUnitTests : Bunit.TestContext
{
    private readonly Mock<IClientViewService> _clientViewServiceMock;

    public ClientGridComponentUnitTests()
    {
        _clientViewServiceMock = new Mock<IClientViewService>();
        Services.AddScoped(services => _clientViewServiceMock.Object);
        Services.AddSyncfusionBlazor();
        Services.AddOptions();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }
}
