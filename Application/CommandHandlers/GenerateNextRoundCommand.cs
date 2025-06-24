namespace Application.CommandHandlers;

public record GenerateNextRoundCommand(
    IEnumerable<string> participants);