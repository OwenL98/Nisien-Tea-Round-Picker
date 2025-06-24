namespace Application.Services;

public class NameSelectorService : INameSelectorService
{
    public string SelectName(IEnumerable<string> name)
    {
        return "joe";
    }
}