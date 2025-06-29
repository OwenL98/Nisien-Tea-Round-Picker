namespace Api.Request;

/// <summary>
/// request for a random participant
/// </summary>
public class GetRandomParticipantRequest
{
    /// <summary>
    /// The participants to select from
    /// </summary>
    public IEnumerable<Participant> Participants { get; init; } = [];
}