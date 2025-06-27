using Application.Domain;

namespace Application.Services;

public interface IParticipantSelectorService
{
    Participant SelectParticipant(IEnumerable<Participant> participant);
}