using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Auth.PasswordChange;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Users.GetCurrentUser;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Users.GetUserInfo;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Users.GetUsersCount;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Users.GetUsersList;

namespace RU.NSU.FIT.VirtualMeetingManager.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Возвращает текущего аутентифицированного пользователя системы
    /// </summary>
    /// <returns></returns>
    [HttpGet("getCurrentUser")]
    public Task<CurrentUserDto> GetCurrentUser()
    {
        return _mediator.Send(new GetCurrentUserQuery(), HttpContext.RequestAborted);
    }

    /// <summary>
    /// Возврщает список всех пользователей системы
    /// </summary>
    [HttpPost("getUsersList")]
    public Task<GetUserListResponse> GetUsersList(GetUsersListQuery query)
    {
        return _mediator.Send(query, HttpContext.RequestAborted);
    }
    
    /// <summary>
    /// Возврщает количество всех пользователей системы
    /// </summary>
    [HttpPost("getUsersCount")]
    public Task<UsersCountDto> GetUsersCount(GetUsersCountQuery query)
    {
        return _mediator.Send(new GetUsersCountQuery(), HttpContext.RequestAborted);
    }

    /// <summary>
    /// Возвращает информацию по пользователю
    /// </summary>
    [HttpPost("getUserInfo")]
    public Task<GetUserInfoResponse> GetUserInfo(GetUserInfoQuery query)
    {
        return _mediator.Send(query, HttpContext.RequestAborted);
    }
    
    /// <summary>
    /// Меняет пароль пользователя
    /// </summary>
    [HttpPost("changePassword")]
    public Task ChangePassword(ChangePasswordCommand command)
    {
        return _mediator.Send(command, HttpContext.RequestAborted);
    }
}