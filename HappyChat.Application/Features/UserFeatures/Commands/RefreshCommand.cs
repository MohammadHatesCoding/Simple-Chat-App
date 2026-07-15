using FluentValidation;
using HappyChat.Application.Features.UserFeatures.DTO;
using MediatR;

namespace HappyChat.Application.Features.UserFeatures.Commands;

public class RefreshCommandValidator : AbstractValidator<RefreshCommand>
{
    public RefreshCommandValidator()
    {
        RuleFor(x => x.command.RefreshToken)
            .NotEmpty();

        RuleFor(x => x.command.UserId)
            .NotEmpty();
    }
}

public record RefreshCommand(RefreshRequest command) : IRequest<RefreshResponse>;