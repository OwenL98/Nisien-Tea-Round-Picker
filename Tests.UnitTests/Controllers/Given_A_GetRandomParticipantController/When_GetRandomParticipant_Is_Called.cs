using Api.Controllers;
using Api.Request;
using Api.Response;
using Application.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Tests.UnitTests.Controllers.Given_A_GetRandomParticipantController;

public class When_GetRandomParticipant_Is_Called
{
    [Fact]
    public void Then_ParticipantSelectorService_Is_Called_With_Expected_Args()
    {
        //Arrange
        var participants = new List<string>
        {
            "John Doe",
            "name 2"
        };
        
        var getRandomParticipantRequest = new GetRandomParticipantRequest()
        {
            Participants = participants
        };

        var mockParticipantSelectorService = CreateParticipantSelectorServiceTestDouble();
        var sut = CreateSut(
            participantSelectorService: mockParticipantSelectorService);

        //Act
        sut.GetRandomParticipant(getRandomParticipantRequest);

        //Assert
        mockParticipantSelectorService
            .Received(1)
            .SelectParticipant(participants);
    }

    [Fact]
    public void Then_Expected_Response_Is_Returned()
    {
        //Arrange
        var participants = new List<string>
        {
            "John Doe",
            "name 2"
        };
        
        var getRandomParticipantRequest = new GetRandomParticipantRequest()
        {
            Participants = participants
        };
        
        var expectedResult = new GetRandomParticipantResponse
        {
            Name = "John Doe",
        };

        var dummyParticipantSelectorService = CreateParticipantSelectorServiceTestDouble();
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
        var participants = Enumerable.Empty<string>();
        
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
        var participants = new List<string>
        {
            "John Doe"
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
    
    private static IParticipantSelectorService CreateParticipantSelectorServiceTestDouble()
    {
        var sub = Substitute.For<IParticipantSelectorService>();

        sub.SelectParticipant(Arg.Any<IEnumerable<string>>())
            .ReturnsForAnyArgs("John Doe");
        
        return sub;
    }
}