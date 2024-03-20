using System.Collections.Immutable;
using LightsOn.BlazorApp.Models.Clients;
using Xeptions;

namespace LightsOn.BlazorApp.Brokers.Apis;

public partial interface IApiBroker
{
    ValueTask<Either<Exception, ImmutableArray<Client>>> GetClients();
}