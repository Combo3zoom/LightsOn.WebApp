using System.Collections.Immutable;
using LanguageExt;
using LightsOn.WebApp.Models.Clients;

namespace LightsOn.WebApp.Services.Foundations.Clients;

public interface IClientService
{
    ValueTask<Either<Exception, ImmutableArray<Client>>> GetClients();
}