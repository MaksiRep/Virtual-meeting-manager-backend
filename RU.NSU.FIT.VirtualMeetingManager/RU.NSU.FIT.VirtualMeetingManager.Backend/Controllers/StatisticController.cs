using MediatR;
using Microsoft.AspNetCore.Mvc;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Statistic.GetUsersCount;

namespace RU.NSU.FIT.VirtualMeetingManager.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticController : ControllerBase
{
    private readonly IMediator _mediator;

    public StatisticController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Возврщает количество всех пользователей системы
    /// </summary>
    [HttpGet("getUsersCount")]
    public Task<int> GetUsersCount()
    {
        return _mediator.Send(new GetUsersCountQuery(), HttpContext.RequestAborted);
    }
}