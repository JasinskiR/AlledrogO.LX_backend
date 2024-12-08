using AlledrogO.Post.Domain.ValueObjects;
using Shouldly;

namespace AlledrogO.UnitTests.Domain;

public class AuthorDetailsTests
{
    [Theory]
    [InlineData("correct@email.com", "123456789")]
    [InlineData("correct@email.com", "+48123456789")]
    [InlineData("correct@email.com", "+48 123 456 789")]
    public void CreateAuthorDetails_CorrectPhoneNumber_Should_CreateAuthorDetailsSuccessfully(
        string email, 
        string phoneNumber)
    {
        // Arrange
        var exception = Record.Exception(() => new AuthorDetails(email, phoneNumber));
        
        // Assert
        exception.ShouldBeNull();
    }
}