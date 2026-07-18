using HappyChat.Application.Contracts;
using HappyChat.Application.Contracts.Services;
using HappyChat.Application.Features.UserFeatures.DTO;
using HappyChat.Core.Models;
using MediatR;

namespace HappyChat.Application.Features.UserFeatures.Commands;

public class CheckOTPCommandHandler : IRequestHandler<CheckOTPCommand, CheckOTPResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOTPService _otpService;
    private readonly ITokenService _tokenService;
    private readonly IJWTService _jWTService;
    public CheckOTPCommandHandler(IUnitOfWork unitOfWork, IOTPService otpService, ITokenService tokenService, IJWTService jWTService)
    {
        _unitOfWork = unitOfWork;
        _otpService = otpService;
        _tokenService = tokenService;
        _jWTService = jWTService;
    }
    public async Task<CheckOTPResponse> Handle(CheckOTPCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository
            .GetByPhoneNumberWithRolesAsync(request.command.PhoneNumber);

        bool check = _otpService.VerifyOtpAsync(user.OtpHash, request.command.otp);

        if (check && user.OTPDuration < DateTime.UtcNow)
            throw new Exception(message: "OTP is expired!");

        if (!check && DateTime.UtcNow < user.OTPDuration)
            throw new Exception(message: "Invalid OTP!");

        if (check && DateTime.UtcNow < user.OTPDuration)
        {
            user.OtpHash = string.Empty;
            user.OTPDuration = null;
        }

        var oldTokens = await _unitOfWork.RefreshTokenRepository.GetAllActiveTokensByUserId(user.Id);

        foreach (var oldToken in oldTokens)
            oldToken.IsRevoked = true;

        var accessToken = _jWTService.GenerateAccessToken(user);

        var rawRefreshToken = _jWTService.GenerateRefreshToken();

        var hashedRefreshToken = _tokenService.Hash(rawRefreshToken);

        var refreshTokenEntity = new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = hashedRefreshToken,
            UserId = user.Id
        };

        await _unitOfWork.RefreshTokenRepository.CreateAsync(refreshTokenEntity);

        return new CheckOTPResponse(AccessToken: accessToken, RefreshToken: rawRefreshToken,
                AccessTokenExpiresAt: DateTime.UtcNow.AddMinutes(30));
    }
}
