using StarWarsFleet.Application.Shared.UseCases.Abstractions;

namespace StarWarsFleet.Application.Factions.UseCases.Update;

public class Command : IRequest
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}