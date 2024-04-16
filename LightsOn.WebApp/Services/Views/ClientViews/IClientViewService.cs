using System.Collections.Immutable;
using LanguageExt;
using LightsOn.WebApp.Models.ClientViews;

namespace LightsOn.WebApp.Services.Views.ClientViews;

public interface IClientViewService
{
    ValueTask<Either<Exception, ImmutableArray<ClientView>>> GetClientViews();
}