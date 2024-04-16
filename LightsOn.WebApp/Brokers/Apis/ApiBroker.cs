using System.Collections.Immutable;
using LanguageExt;
using LightsOn.WebApp.HttpClients.ApiHttpClient;
using LightsOn.WebApp.Models.Clients;
using static LanguageExt.Prelude;

namespace LightsOn.WebApp.Brokers.Apis;

public partial class ApiBroker : IApiBroker
{
    private readonly IApiHttpClient _apiClient;

    public ApiBroker(IApiHttpClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async ValueTask<Either<Exception, ImmutableArray<Client>>> GetClients()
    {
        try
        {
            var clients = await _apiClient.GetContentAsync<ImmutableArray<Client>>("");
            return Right(clients);
        }
        catch (Exception e)
        {
            return Left(e);
        }
    }
}