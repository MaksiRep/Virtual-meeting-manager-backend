using MediatR;
using Microsoft.AspNetCore.Mvc;
using RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Auth.Base;
using RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Auth.ForgotPassword;
using RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Auth.RefreshToken;
using RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Auth.Registration;
using RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Auth.ResetPassword;
using RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Auth.SignIn;

namespace RU.NSU.FIT.VirtualMeetingManager.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    
    /// <summary>
    /// Регистрирует нового пользователя в системе
    /// </summary>
    [HttpPost("registration")]
    public Task<AuthResponse> Registration(RegistrationCommand command)
    {
        return _mediator.Send(command, HttpContext.RequestAborted);
    }
    
    /// <summary>
    /// Сбрасывает текущий пароль пользователя
    /// </summary>
    [HttpPost("resetPassword")]
    public Task ResetPassword(ResetPasswordCommand command)
    {
        return _mediator.Send(command, HttpContext.RequestAborted);
    }

    /// <summary>
    /// Формирует письмо с токеном для сброса пароля
    /// </summary>
    [HttpPost("forgotPassword")]
    public Task ForgotPassword(ForgotPasswordCommand command)
    {
        return _mediator.Send(command, HttpContext.RequestAborted);
    }
}