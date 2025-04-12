using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarWarsFleet.Domain.Entities;
using StarWarsFleet.Domain.Models;
using StarWarsFleet.Infrastructure.Data;

namespace StarWarsFleet.Web.Controllers;

[Route("faction")]
public class FactionController(StarWarsDbContext context) : Controller
{
    [HttpPost]
    public async Task<IActionResult> AddFaction([FromBody] FactionViewModel model)
    {
        var faction = new FactionEntity { Id = Guid.NewGuid(), Name = model.Name };
        await context.Factions.AddAsync(faction);
        await context.SaveChangesAsync();
        return Ok(faction);
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