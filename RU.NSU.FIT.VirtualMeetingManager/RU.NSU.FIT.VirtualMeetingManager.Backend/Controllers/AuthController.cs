using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Auth.Base;
using RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Auth.RefreshToken;
using RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Auth.SignIn;

namespace RU.NSU.FIT.VirtualMeetingManager.Backend.Controllers;

[ApiController]
[Route("api/[controller]")] //  /api/Auth/
public class AuthController : Controller
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Аутентифицирует пользователя
    /// </summary>
    [HttpPost("signIn")]
    public Task<AuthResponse> SignIn(SignInCommand command)
    {
        return _mediator.Send(command, HttpContext.RequestAborted);
    }

    /// <summary>
    /// Обновляет токен доступа
    /// </summary>
    [HttpPost("refreshToken")]
    public Task<AuthResponse> RefreshToken(RefreshTokenCommand command)
    {
        return _mediator.Send(command, HttpContext.RequestAborted);
    }

    [Authorize]
    [HttpGet("testAuth")]
    public IActionResult TestAuth()
    {
        return Ok();
    }

    [Authorize(Roles = $"{RoleConstants.Admin}")]
    [HttpGet("testAuthAdmin")]
    public IActionResult TestAuthAdmin()
    {
        return Ok();
    }

    [Authorize(Roles = $"{RoleConstants.User}")]
    [HttpGet("testAuthUser")]
    public IActionResult TestAuthUser()
    {
        return Ok();
    }
}