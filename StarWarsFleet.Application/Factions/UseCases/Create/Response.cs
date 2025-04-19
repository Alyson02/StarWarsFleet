using StarWarsFleet.Application.Shared.UseCases.Abstractions;

namespace StarWarsFleet.Application.Factions.UseCases.Create;

public record Response(string Message) : IResponse
{
    public string Message { get; set; } = Message;
}