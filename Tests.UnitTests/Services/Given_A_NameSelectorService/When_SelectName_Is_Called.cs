using Application.Services;
using FluentAssertions;
using Xunit;

namespace Tests.UnitTests.Services.Given_A_NameSelectorService;

public class When_SelectName_Is_Called
{
    [Fact]
    public void Then_Expected_Name_Is_Returned()
    {
        //Arrange
        var participants = new []{"a", "b"};
        var sut = CreateSut();

        //Act
        var result = sut.SelectName(participants);

        //Assert
        result.Should().BeEquivalentTo("joe");
    }

    private static NameSelectorService CreateSut()
    {
        return new NameSelectorService(
        );
    }
}