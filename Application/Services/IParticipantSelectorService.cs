namespace Application.Services;

public interface IParticipantSelectorService
{
    string SelectParticipant(IEnumerable<string> name);
}