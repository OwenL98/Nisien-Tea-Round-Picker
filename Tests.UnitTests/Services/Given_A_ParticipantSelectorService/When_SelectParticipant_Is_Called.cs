using Application.Domain;
using Application.Services;
using Common.Facades;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Tests.UnitTests.Services.Given_A_ParticipantSelectorService;

public class When_SelectParticipant_Is_Called
{
    [Fact]
    public void Then_Facade_Is_Called()
    {
        //Arrange
        var participants = new []
        {
            new Participant{Name = "Joe"},
            new Participant{Name = "Darren"}
        };
        var mockRandomFacade = CreateRandomFacadeTestDouble();
        var sut = CreateSut(randomFacade: mockRandomFacade);

        //Act
        sut.SelectParticipant(participants);

        //Assert
        mockRandomFacade.Received(1).GetRandomNumber(2);
    }
    
    [Fact]
    public void Then_Expected_Name_Is_Returned()
    {
        //Arrange
        var participants = new []
        {
            new Participant{Name = "Joe"},
            new Participant{Name = "Darren"}
        };
        var dummyRandomFacade = CreateRandomFacadeTestDouble();
        
        var sut = CreateSut(
            randomFacade: dummyRandomFacade);

        //Act
        var result = sut.SelectParticipant(participants);

        //Assert
        result.Should().BeEquivalentTo(new Participant{Name = "Darren"});
    }

    private static ParticipantSelectorService CreateSut(
        IRandomFacade? randomFacade = null)
    {
        return new ParticipantSelectorService(
            randomFacade: randomFacade ?? CreateRandomFacadeTestDouble()
        );
    }

    private static IRandomFacade CreateRandomFacadeTestDouble()
    {
        var sub = Substitute.For<IRandomFacade>();
        
        sub.GetRandomNumber(Arg.Any<int>())
            .ReturnsForAnyArgs(1);

        return sub;
    }
}