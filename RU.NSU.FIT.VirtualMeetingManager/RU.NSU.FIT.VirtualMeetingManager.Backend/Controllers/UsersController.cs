using MediatR;
using Microsoft.AspNetCore.Mvc;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.GetUsers;

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

    [HttpPost("getUsers")]
    public Task<ICollection<UserDto>> GetUsersList(GetUsersListQuery query)
    {
        return _mediator.Send(query, HttpContext.RequestAborted);
    }
}