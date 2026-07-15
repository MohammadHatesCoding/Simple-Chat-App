using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HappyChat.Api.Controllers.Base;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    private readonly IMediator _mediatr;
    public BaseController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }
}