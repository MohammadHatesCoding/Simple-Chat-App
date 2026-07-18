using AutoMapper;
using HappyChat.Application.Contracts;
using HappyChat.Application.Contracts.Services;
using HappyChat.Application.Features.UserFeatures.DTO;
using HappyChat.Core.Models;
using MediatR;

namespace HappyChat.Application.Features.UserFeatures.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOTPService _oTPService;

    public RegisterCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IOTPService oTPService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _oTPService = oTPService;
    }
    public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = _mapper.Map<User>(request.command);

            await _unitOfWork.UserRepository.AddAsync(user);

            var role = (await _unitOfWork.RoleRepository.FindAsync(x => x.Title == "User")).FirstOrDefault();

            await _unitOfWork.UserRoleRepository.CreateAsync(user.Id, role.Id);

            var otp = _oTPService.GenerateOtpAsync();

            user.OtpHash = otp.hashedOtp;

            user.OTPDuration = DateTime.UtcNow.AddSeconds(60);

            Console.WriteLine($"Your otp is: {otp.rawOtp}");

            await _unitOfWork.CommitAsync(cancellationToken);

            return new RegisterResponse(Id: user.Id);
        }
        catch (Exception ex)
        {
            throw new Exception(message: ex.Message);
        }
    }
}