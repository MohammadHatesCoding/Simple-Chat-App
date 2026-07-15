using HappyChat.Application.Contracts;
using HappyChat.Application.Contracts.Services;
using HappyChat.Application.Features.UserFeatures.DTO;
using MediatR;

namespace HappyChat.Application.Features.UserFeatures.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOTPService _oTPService;
    public LoginCommandHandler(IUnitOfWork unitOfWork, IOTPService oTPService)
    {
        _unitOfWork = unitOfWork;
        _oTPService = oTPService;
    }
    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = (await _unitOfWork.UserRepository
                .FindAsync(x => x.Username.ToLower() == request.command.PhoneNumber.ToLower())).FirstOrDefault();

            if (user == null || !user.IsActive)
                throw new UnauthorizedAccessException();

            var otp = _oTPService.GenerateOtpAsync();

            user.OtpHash = otp.hashedOtp;

            user.OTPDuration = DateTime.UtcNow.AddSeconds(60);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new LoginResponse(Success: true);
        }
        catch (Exception ex)
        {
            throw new Exception(message: ex.Message);
        }
    }
}