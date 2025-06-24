using Api.Controllers;
using Api.Models;
using Application.CommandHandlers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Tests.UnitTests.Controllers.Given_A_GetNextRoundController;

public class When_GetNextRound_Is_Called
{
    [Fact]
    public void Then_CommandHandler_Is_Called()
    {
        //Arrange
        var participants = new List<string>
        {
            "John Doe",
            "name 2"
        };
        var command = new GenerateNextRoundCommand(participants);
    
        var mockCommandHandler = CreateGenerateNextRoundCommandHandlerTestDouble();
        var sut = CreateSut(mockCommandHandler);
    
        //Act
        sut.GenerateNextRound(participants);
    
        //Assert
        mockCommandHandler.Received(1).Handle(command);
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
        var command = new GenerateNextRoundCommand(participants);

        var expectedResult = new GenerateNextRoundResponse
        {
            Name = participants[0],
        };
    
        var dummyCommandHandler = CreateGenerateNextRoundCommandHandlerTestDouble(command);
        var sut = CreateSut(dummyCommandHandler);
    
        //Act
        var result = sut.GenerateNextRound(participants);
    
        //Assert
        result.Should().BeEquivalentTo(new OkObjectResult(expectedResult));
    }

    private static GetNextRoundController CreateSut(
        IGenerateNextRoundCommandHandler? commandHandler = null)
    {
        return new GetNextRoundController(
            commandHandler ?? CreateGenerateNextRoundCommandHandlerTestDouble()
        );
    }

    private static IGenerateNextRoundCommandHandler CreateGenerateNextRoundCommandHandlerTestDouble(
        GenerateNextRoundCommand? command = null)
    {
        var sub = Substitute.For<IGenerateNextRoundCommandHandler>();
        
        sub.Handle(Arg.Any<GenerateNextRoundCommand>())
            .ReturnsForAnyArgs(command.participants.FirstOrDefault() ?? "John Doe");
        
        return sub;
    }
    
}