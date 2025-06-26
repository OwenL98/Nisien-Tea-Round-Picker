namespace Api.Request;

public class GetRandomParticipantRequest
{
    public IEnumerable<string> Participants { get; init; } = [];
}