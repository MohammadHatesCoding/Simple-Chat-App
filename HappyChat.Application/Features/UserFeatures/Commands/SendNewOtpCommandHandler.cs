using AutoMapper;
using HappyChat.Application.Contracts;
using HappyChat.Application.Contracts.Services;
using HappyChat.Application.Features.UserFeatures.DTO;
using HappyChat.Core.Models;
using MediatR;

namespace HappyChat.Application.Features.UserFeatures.Commands;

public class SendNewOtpCommandHandler : IRequestHandler<SendNewOtpCommand, SendNewOtpResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOTPService _oTPService;
    public SendNewOtpCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IOTPService oTPService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _oTPService = oTPService;
    }

    public async Task<SendNewOtpResponse> Handle(SendNewOtpCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = (await _unitOfWork.UserRepository
                .FindAsync(x => x.PhoneNumber.ToLower() == request.command.PhoneNumber.ToLower())).FirstOrDefault();

            var otp = _oTPService.GenerateOtpAsync();

            user.OtpHash = otp.hashedOtp;

            user.OTPDuration = DateTime.UtcNow.AddSeconds(60);

            Console.WriteLine($"Your otp is: {otp.rawOtp}");

            return new SendNewOtpResponse(Success: true);
        }
        catch (Exception ex)
        {
            throw new Exception(message: ex.Message);
        }
    }
}
