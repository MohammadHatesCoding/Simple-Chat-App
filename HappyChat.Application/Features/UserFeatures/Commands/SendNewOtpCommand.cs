using HappyChat.Application.Features.UserFeatures.DTO;
using MediatR;

namespace HappyChat.Application.Features.UserFeatures.Commands;

public record SendNewOtpCommand(SendNewOtpRequest command) : IRequest<SendNewOtpResponse>;