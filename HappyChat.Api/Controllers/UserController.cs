using HappyChat.Api.Controllers.Base;
using HappyChat.Application.Features.UserFeatures.Commands;
using HappyChat.Application.Features.UserFeatures.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HappyChat.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : BaseController
{
    private readonly IMediator _mediatr;
    public UserController(IMediator mediatr) : base(mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpPost]
    [Route(nameof(Register))]
    public async Task<RegisterResponse> Register(RegisterCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send<RegisterResponse>(request, cancellationToken);

        if (result is null)
            throw new Exception();

        return result;
    }

    [HttpPost]
    [Route(nameof(Login))]
    public async Task<LoginResponse> Login(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send<LoginResponse>(request, cancellationToken);

        if (result is null)
            throw new Exception();

        return result;
    }

    [HttpPost]
    [Route(nameof(Logout))]
    [Authorize("SysAdmin")]
    public async Task<LogoutResponse> Logout(LogoutCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send<LogoutResponse>(request, cancellationToken);

        if (result is null)
            throw new Exception();

        return result;
    }

    [HttpPost]
    [Route(nameof(Refresh))]
    public async Task<RefreshResponse> Refresh(RefreshCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send<RefreshResponse>(request, cancellationToken);

        if (result is null)
            throw new Exception();

        return result;
    }

    [HttpPost]
    [Route(nameof(CheckOtp))]
    public async Task<CheckOTPResponse> CheckOtp(CheckOTPCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send<CheckOTPResponse>(request, cancellationToken);

        if (result is null)
            throw new Exception();

        return result;
    }

    [HttpPost]
    [Route(nameof(SendNewOtp))]
    public async Task<SendNewOtpResponse> SendNewOtp(SendNewOtpCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send<SendNewOtpResponse>(request, cancellationToken);

        if (result is null)
            throw new Exception();

        return result;
    }
}