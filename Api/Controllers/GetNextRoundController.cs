using Api.Models;
using Application.CommandHandlers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class GetNextRoundController(
    IGenerateNextRoundCommandHandler commandHandler) : ControllerBase
{
    [HttpPost("v1/generate-next-round")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public IActionResult GenerateNextRound(
        [FromBody] IEnumerable<string> roundParticipants)
    {
        var command = new GenerateNextRoundCommand(
            roundParticipants);
        
        var nextRoundIsOn = commandHandler.Handle(command);

        var response = new GenerateNextRoundResponse
        {
            Name = nextRoundIsOn
        };
        
        return Ok(response);
    }
}//TODO: rename all of the variables + change route etc