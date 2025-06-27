using Api.Request;
using Api.Response;
using Application.Domain;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
public class GetRandomParticipantController(
    IParticipantSelectorService participantSelectorService) : ControllerBase
{
    /// <summary>
    /// Randomly select a participant from the given list of names
    /// </summary>
    /// <param name="getRandomParticipantRequest"></param>
    /// <returns code= "200">OK</returns>
    /// <returns code= "400">BadRequest</returns>
    [ApiExplorerSettings(GroupName = "GetRandomParticipant")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("v1/random/participant")]
    public IActionResult GetRandomParticipant(
        [FromBody] GetRandomParticipantRequest getRandomParticipantRequest)
    {
        if (!IsValid(getRandomParticipantRequest))
        {
            return BadRequest();
        }

        var participants = getRandomParticipantRequest
            .Participants
            .Select(participant => 
                new Participant { Name = participant }).ToList();

        var selectedParticipant = participantSelectorService
            .SelectParticipant(participants);

        var response = new GetRandomParticipantResponse
        {
            Name = selectedParticipant.Name
        };

        return Ok(response);
    }

    private static bool IsValid(GetRandomParticipantRequest getRandomParticipantRequest)
    {
        var participants = getRandomParticipantRequest.Participants;
        if (
            participants.Equals(Enumerable.Empty<string>())
            || getRandomParticipantRequest.Equals(new GetRandomParticipantRequest())
            || participants.Count() <= 1)
        {
            return false;
        }

        return true;
    }
}