using System.Collections.Immutable;
using LightsOn.BlazorApp.Models.Clients;


using LightsOn.BlazorApp.Brokers.Apis;

namespace LightsOn.BlazorApp.Services.Foundations.Clients;

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