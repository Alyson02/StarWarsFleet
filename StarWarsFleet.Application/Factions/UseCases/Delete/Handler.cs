﻿using Microsoft.EntityFrameworkCore;
using StarWarsFleet.Application.Shared.UseCases.Abstractions;
using StarWarsFleet.Domain.Entities;
using StarWarsFleet.Domain.Models;
using StarWarsFleet.Infrastructure.Data;

namespace StarWarsFleet.Application.Factions.UseCases.Delete;

public record Handler : IHandler<Command, FactionEntity>
{
    private readonly StarWarsDbContext _dbContext;

    public Handler(StarWarsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseModel<FactionEntity>> HandleAsync(Command request, CancellationToken cancellationToken = default)
    {
        var faction = await GetFaction(request.Id);
        
        if (faction is null) throw new KeyNotFoundException();
        
        _dbContext.Factions.Remove(faction);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return ResponseModel<FactionEntity>.CreateSuccess(faction, "Faction deleted");
    }

    private async Task<FactionEntity?> GetFaction(string factionId)
    {
        var faction = await _dbContext.Factions.FirstOrDefaultAsync(x => x.Id.ToString() == factionId);
        return faction;
    }
}