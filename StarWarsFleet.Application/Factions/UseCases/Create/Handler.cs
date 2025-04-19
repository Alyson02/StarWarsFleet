using StarWarsFleet.Application.Shared.UseCases.Abstractions;
using StarWarsFleet.Domain.Entities;
using StarWarsFleet.Infrastructure.Data;

namespace StarWarsFleet.Application.Factions.UseCases.Create;

public record Handler : IHandler<Command, Response>
{
    private readonly StarWarsDbContext _dbContext;

    public Handler(StarWarsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response> HandleAsync(Command request, CancellationToken cancellationToken = default)
    {
        await _dbContext.AddAsync(new FactionEntity
        {
            Name = request.Name
        }, cancellationToken);
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new Response("Faction created");
    }
}