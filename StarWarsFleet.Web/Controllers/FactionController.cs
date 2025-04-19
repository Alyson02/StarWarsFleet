using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarWarsFleet.Application.Factions.UseCases.Create;
using StarWarsFleet.Domain.Entities;
using StarWarsFleet.Domain.Models;
using StarWarsFleet.Infrastructure.Data;

namespace StarWarsFleet.Web.Controllers;

[Route("faction")]
public class FactionController(StarWarsDbContext context, Handler handler, Application.Factions.UseCases.Delete.Handler deleteHanlder) : Controller
{
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
        try
        {
            return Ok(await deleteHanlder.HandleAsync(new Application.Factions.UseCases.Delete.Command(){ Id = factionId }));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(ResponseModel.FromException(e));
        }
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