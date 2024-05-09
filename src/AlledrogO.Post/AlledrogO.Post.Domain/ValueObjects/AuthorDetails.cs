using System.ComponentModel.DataAnnotations;
using AlledrogO.Post.Domain.ValueObjects.Exceptions;
using FluentValidation;

namespace AlledrogO.Post.Domain.ValueObjects;

public record AuthorDetails
{
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    
    public AuthorDetails(string email, string phoneNumber)
    {
        var authorDetails = new AuthorDetails
        {
            Email = email,
            PhoneNumber = phoneNumber
        };
        var validator = new AuthorDetailsValidator();
        var result = validator.Validate(authorDetails);
        if (!result.IsValid)
        {
            var failure = result.Errors.FirstOrDefault();
            if (failure.PropertyName == nameof(Email))
            {
                throw new AuthorDataInvalidEmailException(email);
            }
            else if (failure.PropertyName == nameof(PhoneNumber))
            {
                throw new AuthorDataInvalidPhoneNumberException(phoneNumber);
            }
        }
        Email = email;
        PhoneNumber = phoneNumber;
    }

    private AuthorDetails()
    {
    }

    private class AuthorDetailsValidator : AbstractValidator<AuthorDetails>
    {
        public AuthorDetailsValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(@"^\d{9}$");
        }
    }
    
}