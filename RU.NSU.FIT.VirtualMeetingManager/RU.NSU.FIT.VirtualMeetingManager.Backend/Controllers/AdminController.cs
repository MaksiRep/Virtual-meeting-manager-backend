using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Base;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Statistic.GetRolesList;

namespace RU.NSU.FIT.VirtualMeetingManager.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = $"{RoleConstants.Admin}")]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Возврщает список ролей системы
    /// </summary>
    [HttpGet("getRoleList")]
    public Task<ICollection<RoleDto>> GetRoleList()
    {
        return _mediator.Send(new GetRoleListQuery(), HttpContext.RequestAborted);
    }
}