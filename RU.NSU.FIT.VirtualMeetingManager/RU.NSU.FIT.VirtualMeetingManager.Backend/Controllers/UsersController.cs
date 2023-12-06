﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Users.GetCurrentUser;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Users.GetUserInfo;
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
    /// Возвращает информацию по пользователю
    /// </summary>
    [HttpPost("getUserInfo")]
    public Task<GetUserInfoResponse> GetUserInfo(GetUserInfoQuery query)
    {
        return _mediator.Send(query, HttpContext.RequestAborted);
    }
}