using MediatR;
using Microsoft.AspNetCore.Mvc;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.GetMeetings;

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
    
    [HttpPost("getMeetings")]
    public Task<GetMeetingsListResponse> GetMeetingsList(GetMeetingsListQuery query)
    {
        return _mediator.Send(query);
    }
}