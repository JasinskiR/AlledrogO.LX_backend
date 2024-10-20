using FluentValidation;

namespace AlledrogO.User.Api.DTOs;

public class RegisterEventDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}