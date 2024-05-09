using System.ComponentModel.DataAnnotations;
using AlledrogO.Post.Domain.ValueObjects.Exceptions;

namespace AlledrogO.Post.Domain.ValueObjects;

public record AuthorDetails
{
    [EmailAddress]
    private string Email { get; }
    [Phone]
    private string PhoneNumber { get; }
    
    public AuthorDetails(string email, string phoneNumber)
    {
        ValidationContext emailContext = new ValidationContext(this, null, null)
        {
            MemberName = nameof(Email)
        };
        
        List<ValidationResult> emailResults = new();
        if (!Validator.TryValidateProperty(email, emailContext, emailResults))
        {
            throw new AuthorDataInvalidEmailException(email);
        }
        
        ValidationContext phoneContext = new ValidationContext(this, null, null)
        {
            MemberName = nameof(PhoneNumber)
        };
        
        List<ValidationResult> phoneResults = new();
        if (!Validator.TryValidateProperty(phoneNumber, phoneContext, phoneResults))
        {
            throw new AuthorDataInvalidPhoneNumberException(phoneNumber);
        }

        Email = email;
        PhoneNumber = phoneNumber;
    }
    
}