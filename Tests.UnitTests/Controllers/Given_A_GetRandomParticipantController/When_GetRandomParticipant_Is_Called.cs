using Api.Controllers;
using Api.Request;
using Api.Response;
using Application.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;
using domainParticipant = Application.Domain.Participant;

namespace Tests.UnitTests.Controllers.Given_A_GetRandomParticipantController;

public class When_GetRandomParticipant_Is_Called
{
    [Fact]
    public void Then_ParticipantSelectorService_Is_Called_With_Expected_Args()
    {
        //Arrange
        var participants = ExampleParticipants();
        
        var getRandomParticipantRequest = new GetRandomParticipantRequest
        {
            Participants = participants
        };
        
        var expectedParticipants = new []
        {
            new domainParticipant{Name = "John Doe"}, 
            new domainParticipant{Name = "name 2"}
        };

        var mockParticipantSelectorService = CreateParticipantSelectorServiceTestDouble();

        var actual = new List<domainParticipant>();
        mockParticipantSelectorService
            .SelectParticipant(
                Arg.Do<List<domainParticipant>>(arg => actual = arg));
        
        var sut = CreateSut(
            participantSelectorService: mockParticipantSelectorService);

        //Act
        sut.GetRandomParticipant(getRandomParticipantRequest);

        //Assert
        mockParticipantSelectorService
            .Received(1)
            .SelectParticipant(Arg.Any<List<domainParticipant>>());
        
        actual.Should().BeEquivalentTo(expectedParticipants);
    }



    [Fact]
    public void Then_Expected_Response_Is_Returned()
    {
        //Arrange
        var participants = ExampleParticipants();
        
        var getRandomParticipantRequest = new GetRandomParticipantRequest
        {
            Participants = participants
        };
        
        var expectedResult = new GetRandomParticipantResponse
        {
            Name = "John Doe",
        };
        
        var returnedParticipant = new domainParticipant
        {
            Name = "John Doe"
        };

        var dummyParticipantSelectorService = CreateParticipantSelectorServiceTestDouble(returnedParticipant);
        var sut = CreateSut(dummyParticipantSelectorService);

        //Act
        var result = sut.GetRandomParticipant(getRandomParticipantRequest);

        //Assert
        result
            .Should()
            .BeEquivalentTo(new OkObjectResult(expectedResult));
    }
    
    [Fact]
    public void With_Empty_Participants_Then_Expected_Response_Is_Returned()
    {
        //Arrange
        var participants = Enumerable.Empty<Participant>();
        
        var getRandomParticipantRequest = new GetRandomParticipantRequest
        {
            Participants = participants
        };
        
        var sut = CreateSut();

        //Act
        var result = sut.GetRandomParticipant(getRandomParticipantRequest);

        //Assert
        result
            .Should()
            .BeEquivalentTo(new BadRequestResult());
    }
    
    [Fact]
    public void With_Empty_Request_Then_Expected_Response_Is_Returned()
    {
        //Arrange
        var getRandomParticipantRequest = new GetRandomParticipantRequest();
        
        var sut = CreateSut();

        //Act
        var result = sut.GetRandomParticipant(getRandomParticipantRequest);

        //Assert
        result
            .Should()
            .BeEquivalentTo(new BadRequestResult());
    }
    
    [Fact]
    public void With_One_Participant_Then_Expected_Response_Is_Returned()
    {
        //Arrange
        var participants = new List<Participant>
        {
            new()
            {
                Name = "John Doe",
            }
        };
        var getRandomParticipantRequest = new GetRandomParticipantRequest
        {
            Participants = participants
        };
        
        var sut = CreateSut();

        //Act
        var result = sut.GetRandomParticipant(getRandomParticipantRequest);

        //Assert
        result
            .Should()
            .BeEquivalentTo(new BadRequestResult());
    }

    private static GetRandomParticipantController CreateSut(
        IParticipantSelectorService? participantSelectorService = null)
    {
        return new GetRandomParticipantController(
            participantSelectorService ?? CreateParticipantSelectorServiceTestDouble()
        );
    }
    
    private static IParticipantSelectorService CreateParticipantSelectorServiceTestDouble(
        domainParticipant? participant = null)
    {
        var sub = Substitute.For<IParticipantSelectorService>();

        sub.SelectParticipant(Arg.Any<IEnumerable<domainParticipant>>())
            .ReturnsForAnyArgs(participant ?? new domainParticipant{Name = "James Bond"});
        
        return sub;
    }
    
    private static List<Participant> ExampleParticipants()
    {
        return
        [
            new Participant
            {
                Name = "John Doe",
            },

            new Participant
            {
                Name = "name 2"
            }
        ];
    }
}