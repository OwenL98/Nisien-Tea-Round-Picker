namespace Application.CommandHandlers;

public interface IGenerateNextRoundCommandHandler
{
    string Handle(GenerateNextRoundCommand command);
}