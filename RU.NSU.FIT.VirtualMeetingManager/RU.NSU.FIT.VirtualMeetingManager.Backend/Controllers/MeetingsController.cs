using MediatR;
using Microsoft.AspNetCore.Mvc;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Base;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Meetings.GetMeetingsList;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Meetings.GetMeetingUsers;

namespace RU.NSU.FIT.VirtualMeetingManager.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeetingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public MeetingsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("getMeetingsList")]
    public Task<GetMeetingsListResponse> GetMeetingsList(GetMeetingsListQuery query)
    {
        return _mediator.Send(query, HttpContext.RequestAborted);
    }
    
    [HttpPost("getMeetingUsers")]
    public Task<ICollection<UserDto>> GetMeetingUsers(GetMeetingUsersQuery query)
    {
        return _mediator.Send(query, HttpContext.RequestAborted);
    }
}