namespace Api.Request;

/// <summary>
/// request for a random participant
/// </summary>
public class GetRandomParticipantRequest
{
    /// <summary>
    /// The participants to select from
    /// </summary>
    public IEnumerable<string> Participants { get; init; } = [];
}