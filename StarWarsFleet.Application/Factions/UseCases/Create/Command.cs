using StarWarsFleet.Application.Shared.UseCases.Abstractions;

namespace StarWarsFleet.Application.Factions.UseCases.Create;

public class Command : IRequest
{
    public string Name { get; set; } = string.Empty;
}