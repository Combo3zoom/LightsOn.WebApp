using System.Collections.Immutable;
using LanguageExt;
using LightsOn.WebApp.Models.ClientViews;
using LightsOn.WebApp.Services.Foundations.Clients;

namespace LightsOn.WebApp.Services.Views.ClientViews;

public class ClientViewService : IClientViewService
{
    private readonly IClientService _clientService;

    public ClientViewService(
        IClientService clientService)
    {
        _clientService = clientService;
    }

    public async ValueTask<Either<Exception, ImmutableArray<ClientView>>> GetClientViews()
    {
        var getClientsResult = await _clientService.GetClients();
        return getClientsResult.Map(clients => clients
            .Select(client => new ClientView(client.Name))
            .ToImmutableArray());
    }
}