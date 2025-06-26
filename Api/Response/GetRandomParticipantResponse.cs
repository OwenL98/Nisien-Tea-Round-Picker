namespace Api.Response;

/// <summary>
/// The random participant response
/// </summary>
 public class GetRandomParticipantResponse
{
    /// <summary>
    /// The name of the randomly selected participant
    /// </summary>
    public string Name { get; init; } = string.Empty;
}