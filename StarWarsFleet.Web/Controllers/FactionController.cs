using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CreateHandler = StarWarsFleet.Application.Factions.UseCases.Create.Handler;
using DeleteHandler = StarWarsFleet.Application.Factions.UseCases.Delete.Handler;
using UpdateHandler = StarWarsFleet.Application.Factions.UseCases.Update.Handler;
using CreateCommand = StarWarsFleet.Application.Factions.UseCases.Create.Command;
using DeleteCommand = StarWarsFleet.Application.Factions.UseCases.Delete.Command;
using UpdateCommand = StarWarsFleet.Application.Factions.UseCases.Update.Command;
using StarWarsFleet.Domain.Entities;
using StarWarsFleet.Domain.Models;
using StarWarsFleet.Infrastructure.Data;

namespace StarWarsFleet.Web.Controllers;

[Route("faction")]
public class FactionController(StarWarsDbContext context, CreateHandler handler,  DeleteHandler deleteHandler, UpdateHandler updateHandler) : Controller
{
    [HttpPost]
    public async Task<IActionResult> AddFaction([FromBody] CreateCommand command)
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
            return Ok(await deleteHandler.HandleAsync(new DeleteCommand(){ Id = factionId }));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(ResponseModel.FromException(e));
        }
    }

    [HttpPut("{factionId}")]
    public async Task<IActionResult> UpdateFaction([FromRoute] string factionId, [FromBody] FactionViewModel model)
    {
        try
        {
            return Ok(await updateHandler.HandleAsync(new UpdateCommand(){ Id = factionId, Name = model.Name }));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(ResponseModel.FromException(e));
        }
    }
    
    private async Task<FactionEntity?> GetFaction(string factionId)
    {
        var faction = await context.Factions.FirstOrDefaultAsync(x => x.Id.ToString() == factionId);
        return faction;
    }
}