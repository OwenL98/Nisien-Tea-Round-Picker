using Common.Facades;

namespace Application.Services;

public class ParticipantSelectorService(
    IRandomFacade randomFacade ) : IParticipantSelectorService
{
    public string SelectParticipant(IEnumerable<string> name)
    {
        var randomNumber = randomFacade.GetRandomNumber(name.Count());
        
        var selectedName = name.ElementAt(randomNumber);
        
        return selectedName;
    }
}