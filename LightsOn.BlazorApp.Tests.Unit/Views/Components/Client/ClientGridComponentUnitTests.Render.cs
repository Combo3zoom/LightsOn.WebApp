using System.Collections.Immutable;
using Bunit;
using FluentAssertions;
using LanguageExt;
using LightsOn.WebApp.Models.ClientViews;
using LightsOn.WebApp.Models.Views.Components.ClientGrid;
using LightsOn.WebApp.Views.Components.Client;
using Moq;
using NUnit.Framework;

namespace LightsOn.BlazorApp.Tests.Unit.Views.Components.Client;

public partial class ClientGridComponentUnitTests
{
    [Test]
    public void ShouldInitializeComponent()
    {
        // Arrange
        
        // Act
        var cut = new ClientGrid
        {
            ClientViewService = _clientViewServiceMock.Object
        };

        // Assert
        cut.ClientViews.IsNone.Should().BeTrue();
        cut.State.Should().Be(ClientGridState.Loading);
        cut.ErrorMessage.IsNone.Should().BeTrue();
        _clientViewServiceMock.VerifyNoOtherCalls();
    }

    [Test]
    public void ShouldRenderComponent()
    {
        // Arrange
        var clientViews = ImmutableArray.Create<ClientView>();
        var eitherClientViews = Either<Exception, ImmutableArray<ClientView>>.Right(clientViews);
        _clientViewServiceMock.Setup(service => service.GetClientViews())
            .ReturnsAsync(eitherClientViews);

        // Act
        var cut = RenderComponent<ClientGrid>();
        
        // Assert
        cut.WaitForAssertion(() => cut.Instance.State.Should().Be(ClientGridState.Content));
        _clientViewServiceMock.Verify(service => service.GetClientViews(), Times.Once);
        cut.Instance.ClientViews.IsSome.Should().BeTrue();
        cut.Instance.ClientViews.Match(
            views => views.Should().BeEquivalentTo(clientViews), 
            Assert.Fail);
        cut.Instance.State.Should().Be(ClientGridState.Content);
        _clientViewServiceMock.VerifyNoOtherCalls();
    }
}