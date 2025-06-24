using Application.Services;

namespace Application.CommandHandlers;

public class GenerateNextRoundCommandHandler(
    INameSelectorService nameSelectorService) 
    : IGenerateNextRoundCommandHandler
{
    public string Handle(GenerateNextRoundCommand command)
    {
        var selectedName = nameSelectorService.SelectName(command.participants);
        
        return selectedName;
    }
}