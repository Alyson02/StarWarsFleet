using StarWarsFleet.Application.Shared.UseCases.Abstractions;
using StarWarsFleet.Domain.Entities;
using StarWarsFleet.Domain.Models;
using StarWarsFleet.Infrastructure.Data;

namespace StarWarsFleet.Application.Factions.UseCases.Create;

public record Handler : IHandler<Command, FactionEntity>
{
    private readonly StarWarsDbContext _dbContext;

    public Handler(StarWarsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseModel<FactionEntity>> HandleAsync(Command request, CancellationToken cancellationToken = default)
    {
        var inserted = await _dbContext.AddAsync(new FactionEntity
        {
            Name = request.Name
        }, cancellationToken);
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return ResponseModel<FactionEntity>.CreateSuccess(inserted.Entity);
    }

   
}