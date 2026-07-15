namespace HappyChat.Application.Features.UserFeatures.DTO;

public record RegisterRequest(string Name, string LastName, DateTime BirthDate, string PhoneNumber);