using StarWarsFleet.Application.Shared.UseCases.Abstractions;

namespace StarWarsFleet.Application.Factions.UseCases.Delete;

public class Command : IRequest
{
    public string Id { get; set; } = string.Empty;
}