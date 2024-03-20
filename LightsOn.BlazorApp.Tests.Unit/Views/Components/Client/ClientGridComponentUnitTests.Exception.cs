using System.Collections.Immutable;
using Bunit;
using FluentAssertions;
using LanguageExt;
using LightsOn.BlazorApp.Models.ClientViews;
using LightsOn.BlazorApp.Models.Views.Components.ClientGrid;
using LightsOn.BlazorApp.Views.Components.Client;
using Moq;
using NUnit.Framework;

namespace LightsOn.BlazorApp.Tests.Unit.Views.Components.Client;

public partial class ClientGridComponentUnitTests
{
    [Test]
    public void ShouldRenderErrorMessageIfGetClientViewsFails()
    {
        // Arrange
        var randomMessage = "Unexpected exception";
        var expectedMessage = randomMessage;
        var exception = new Exception(randomMessage);
        var eitherClientViews = Either<Exception, ImmutableArray<ClientView>>.Left(exception);
        _clientViewServiceMock.Setup(service => service.GetClientViews())
            .ReturnsAsync(eitherClientViews);

        // Act
        var cut = RenderComponent<ClientGrid>();
        
        // Assert
        cut.WaitForAssertion(() => cut.Instance.State.Should().Be(ClientGridState.Error));
        _clientViewServiceMock.Verify(service => service.GetClientViews(), Times.Once);
        cut.Instance.ClientViews.IsNone.Should().BeTrue();
        cut.Instance.ErrorMessage.IsSome.Should().BeTrue();
        cut.Instance.ErrorMessage.Match(
            actualMessage => actualMessage.Should().BeEquivalentTo(expectedMessage), 
            Assert.Fail);
        _clientViewServiceMock.VerifyNoOtherCalls();
    }
}