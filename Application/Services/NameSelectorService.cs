using Common.Facades;

namespace Application.Services;

public class NameSelectorService(
    IRandomFacade randomFacade ) : INameSelectorService
{
    public string SelectName(IEnumerable<string> name)
    {
        var randomNumber = randomFacade.GetRandomNumber(name.Count());
        
        var selectedName = name.ElementAt(randomNumber);
        
        
        return selectedName;
    }
}