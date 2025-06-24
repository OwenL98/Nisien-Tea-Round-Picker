namespace Application.Services;

public interface INameSelectorService
{
    string SelectName(IEnumerable<string> name);
}