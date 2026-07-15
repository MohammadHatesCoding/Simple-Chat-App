using FluentValidation;
using HappyChat.Application.Features.UserFeatures.DTO;
using MediatR;

namespace HappyChat.Application.Features.UserFeatures.Commands;

public class CheckOTPCommandValidator : AbstractValidator<CheckOTPCommand>
{
    public CheckOTPCommandValidator()
    {
        RuleFor(x => x.command.PhoneNumber)
            .NotEmpty();

        RuleFor(x => x.command.otp)
            .NotEmpty();
    }
}

public record CheckOTPCommand(CheckOTPRequest command) : IRequest<CheckOTPResponse>;