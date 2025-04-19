using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarWarsFleet.Application.Factions.UseCases.Create;
using StarWarsFleet.Domain.Entities;
using StarWarsFleet.Domain.Models;
using StarWarsFleet.Infrastructure.Data;

namespace StarWarsFleet.Web.Controllers;

[Route("faction")]
public class FactionController(StarWarsDbContext context, Handler handler) : Controller
{
    private readonly Handler handler = handler;
    
    [HttpPost]
    public async Task<IActionResult> AddFaction([FromBody] Command command)
    {
        var result = await handler.HandleAsync(command);
        
        return Created($"/faction/{result.Data?.Id}", result.Data );
    }
    
    [HttpGet]
    public async Task<IActionResult> ListFactions()
    {
        return Ok(await context.Factions.ToListAsync());
    }
    
    [HttpDelete("{factionId}")]
    public async Task<IActionResult> DeleteFaction([FromRoute] string factionId)
    {
        var faction = await GetFaction(factionId);
        
        if (faction is null) return NotFound();
        
        context.Factions.Remove(faction);
        await context.SaveChangesAsync();
        
        return Ok();
    }

    [HttpPut("{factionId}")]
    public async Task<IActionResult> DeleteFaction([FromRoute] string factionId, [FromBody] FactionViewModel model)
    {
        var faction = await GetFaction(factionId);

        if (faction is null) return NotFound();

        faction.Name = model.Name;
        
        context.Factions.Update(faction);
        await context.SaveChangesAsync();
        
        return Ok(faction);
    }
    
    private async Task<FactionEntity?> GetFaction(string factionId)
    {
        var faction = await context.Factions.FirstOrDefaultAsync(x => x.Id.ToString() == factionId);
        return faction;
    }
}