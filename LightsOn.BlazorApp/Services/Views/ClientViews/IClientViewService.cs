using System.Collections.Immutable;
using LightsOn.BlazorApp.Models.ClientViews;

namespace LightsOn.BlazorApp.Services.Views.ClientViews;

public interface IClientViewService
{
    ValueTask<Either<Exception, ImmutableArray<ClientView>>> GetClientViews();
}