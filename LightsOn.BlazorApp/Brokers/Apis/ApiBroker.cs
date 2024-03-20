using System.Collections.Immutable;
using LightsOn.BlazorApp.HttpClients.ApiHttpClient;
using LightsOn.BlazorApp.Models.Clients;
using RESTFulSense.Clients;
using Xeptions;

namespace LightsOn.BlazorApp.Brokers.Apis;

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