using System.Collections.Immutable;
using LanguageExt;
using LightsOn.WebApp.Brokers.Apis;
using LightsOn.WebApp.Models.Clients;

namespace LightsOn.WebApp.Services.Foundations.Clients;

public class ClientService : IClientService
{
    private readonly IApiBroker _apiBroker;

    public ClientService(IApiBroker apiBroker)
    {
        _apiBroker = apiBroker;
    }

    public async ValueTask<Either<Exception, ImmutableArray<Client>>> GetClients()
    {
        return await _apiBroker.GetClients();
    }
}