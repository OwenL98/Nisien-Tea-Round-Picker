using Api.Request;
using Api.Response;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using domainParticipant = Application.Domain.Participant;

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

        var participants = MapToDomainParticipants(getRandomParticipantRequest);

        var selectedParticipant = participantSelectorService
            .SelectParticipant(participants);

        var response = new GetRandomParticipantResponse
        {
            Name = selectedParticipant.Name
        };

        return Ok(response);
    }

    private static List<domainParticipant> MapToDomainParticipants(
        GetRandomParticipantRequest getRandomParticipantRequest)
    {
        var participants = getRandomParticipantRequest
            .Participants
            .Select(participant =>
                new domainParticipant
                {
                    Name = participant.Name
                }
            ).ToList();
        
        return participants;
    }

    private static bool IsValid(GetRandomParticipantRequest getRandomParticipantRequest)
    {
        var participants = getRandomParticipantRequest.Participants;
        if (getRandomParticipantRequest.Equals(new GetRandomParticipantRequest()) 
            || participants.Equals(Enumerable.Empty<Participant>())
            || participants.Count() <= 1)
        {
            return false;
        }
        return true;
    }
}