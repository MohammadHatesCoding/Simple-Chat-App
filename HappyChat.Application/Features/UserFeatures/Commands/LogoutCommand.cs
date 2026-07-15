using FluentValidation;
using HappyChat.Application.Features.UserFeatures.DTO;
using MediatR;

namespace HappyChat.Application.Features.UserFeatures.Commands;

public class LogoutCommandValidator : AbstractValidator<LogoutCommand>
{
    public LogoutCommandValidator()
    {
    
    }
}

public record LogoutCommand(LogoutRequest command) : IRequest<LogoutResponse>;