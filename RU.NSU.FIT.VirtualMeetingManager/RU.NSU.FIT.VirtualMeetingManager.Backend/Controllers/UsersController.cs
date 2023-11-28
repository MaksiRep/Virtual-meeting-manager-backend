using MediatR;
using Microsoft.AspNetCore.Mvc;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Users.GetCurrentUser;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Users.GetUsersList;

namespace RU.NSU.FIT.VirtualMeetingManager.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("getCurrentUser")]
    public Task<CurrentUserDto> GetCurrentUser()
    {
        return _mediator.Send(new GetCurrentUserQuery(), HttpContext.RequestAborted);
    }

    [HttpPost("getUsersList")]
    public Task<GetUserListResponse> GetUsersList(GetUsersListQuery query)
    {
        return _mediator.Send(query, HttpContext.RequestAborted);
    }
}