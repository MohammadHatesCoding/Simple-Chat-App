using FluentValidation;
using HappyChat.Application.Features.UserFeatures.DTO;
using MediatR;

namespace HappyChat.Application.Features.UserFeatures.Commands;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.command.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.command.LastName)
            .NotEmpty()
            .MaximumLength(700);

        RuleFor(x => x.command.PhoneNumber)
            .NotEmpty()
            .MaximumLength(13);
    }
}

public record RegisterCommand(RegisterRequest command) : IRequest<RegisterResponse>;