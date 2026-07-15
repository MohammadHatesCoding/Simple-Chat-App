using FluentValidation;
using HappyChat.Application.Features.UserFeatures.DTO;
using MediatR;

namespace HappyChat.Application.Features.UserFeatures.Commands;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.command.PhoneNumber)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.command.PhoneNumber)
            .NotEmpty()
            .NotNull();
    }
}

public record LoginCommand(LoginRequest command) : IRequest<LoginResponse>;