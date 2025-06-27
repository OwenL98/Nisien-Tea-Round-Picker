using Application.Domain;
using Common.Facades;

namespace Application.Services;

public class ParticipantSelectorService(
    IRandomFacade randomFacade ) : IParticipantSelectorService
{
    public Participant SelectParticipant(IEnumerable<Participant> participants)
    {
        var participantsList = participants.ToList();
        var randomNumber = randomFacade.GetRandomNumber(participantsList.Count);
        
        var selectedParticipant = participantsList.ElementAt(randomNumber);
        
        return selectedParticipant;
    }
}