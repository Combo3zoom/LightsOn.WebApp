using System.Collections.Immutable;
using LightsOn.BlazorApp.Models.Clients;

namespace LightsOn.BlazorApp.Services.Foundations.Clients;

public interface IClientService
{
    ValueTask<Either<Exception, ImmutableArray<Client>>> GetClients();
}