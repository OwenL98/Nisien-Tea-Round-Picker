using Application.CommandHandlers;
using Application.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Tests.UnitTests.CommandHandlers.Given_A_GenerateNextRoundCommandHandler;

public class When_Handle_Is_Called
{
    [Fact]
    public void Then_Service_Is_Called_With_Expected_Args()
    {
        //Arrange
        var command = new GenerateNextRoundCommand(["a", "b"]);
        
        var mockNameSelectorService = CreateNameSelectorServiceTestDouble();
        
        var sut = CreateSut(
            nameSelectorService: mockNameSelectorService);

        //Act
        sut.Handle(command);

        //Assert
        mockNameSelectorService
            .Received(1)
            .SelectName(command.participants);
    }

    [Fact]
    public void Then_Handler_Returns_Expected_Name()
    {
        //Arrange
        var command = new GenerateNextRoundCommand(["a", "b"]);

        var expectedName = "b";
        
        var dummyNameSelectorService = CreateNameSelectorServiceTestDouble(expectedName);
        
        var sut = CreateSut(
            nameSelectorService: dummyNameSelectorService);

        //Act
        var result = sut.Handle(command);

        //Assert
        result.Should().BeEquivalentTo(expectedName);
    }
    
    private static GenerateNextRoundCommandHandler CreateSut(
        INameSelectorService? nameSelectorService = null)
    {
        return new GenerateNextRoundCommandHandler(
            nameSelectorService ?? CreateNameSelectorServiceTestDouble()
        );
    }

    private static INameSelectorService CreateNameSelectorServiceTestDouble(
        string? name = null)
    {
        var sub = Substitute.For<INameSelectorService>();

        sub.SelectName(
                Arg.Any<IEnumerable<string>>())
            .Returns(name ?? "James");

        return sub;
    }
}