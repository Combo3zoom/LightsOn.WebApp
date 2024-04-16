using System.Collections.Immutable;
using LanguageExt;
using LightsOn.WebApp.Models.Clients;

namespace LightsOn.WebApp.Brokers.Apis;

public partial interface IApiBroker
{
    ValueTask<Either<Exception, ImmutableArray<Client>>> GetClients();
}