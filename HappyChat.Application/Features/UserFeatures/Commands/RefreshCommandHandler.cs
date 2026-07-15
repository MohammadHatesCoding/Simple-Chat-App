using HappyChat.Application.Contracts;
using HappyChat.Application.Contracts.Services;
using HappyChat.Application.Features.UserFeatures.DTO;
using HappyChat.Core.Models;
using MediatR;

namespace HappyChat.Application.Features.UserFeatures.Commands;

public class RefreshCommandHandler : IRequestHandler<RefreshCommand, RefreshResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJWTService _jWTService;
    private readonly ITokenService _tokenService;
    public RefreshCommandHandler(IUnitOfWork unitOfWork, IJWTService jWTService, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _jWTService = jWTService;
        _tokenService = tokenService;
    }
    public async Task<RefreshResponse> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var hashedInputToken = _tokenService.Hash(request.command.RefreshToken);

            var existingToken = await _unitOfWork.RefreshTokenRepository.GetByTokenHashAsync(hashedInputToken);

            if (existingToken == null &&
                existingToken.IsRevoked &&
                existingToken.ExpiresAt <= DateTime.UtcNow)
                throw new UnauthorizedAccessException("Invalid refresh token");

            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.command.UserId);

            if (user == null || !user.IsActive)
                throw new UnauthorizedAccessException();

            existingToken.IsRevoked = true;

            var newRawRefreshToken = _jWTService.GenerateRefreshToken();
            var newHashedRefreshToken = _tokenService.Hash(newRawRefreshToken);

            var refreshTokenEntity = new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = newHashedRefreshToken,
                UserId = user.Id
            };

            await _unitOfWork.RefreshTokenRepository.CreateAsync(refreshTokenEntity);

            await _unitOfWork.CommitAsync(cancellationToken);

            var newAccessToken = _jWTService.GenerateAccessToken(user);

            return new RefreshResponse(AccessToken: newAccessToken, RefreshToken: newRawRefreshToken, 
                AccessTokenExpiresAt: DateTime.UtcNow.AddMinutes(30));
        }
        catch (Exception ex) 
        {
            throw new Exception(message: ex.Message);
        }
    }
}