using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Meetings.CancelMeetingVisiting;
using RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Meetings.CreateMeeting;
using RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Meetings.DeleteMeeting;
using RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Meetings.UpdateMeeting;
using RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Meetings.UpdateMeetingImage;
using RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Meetings.VisitMeeting;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Base;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Meetings.GetMeetingImage;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Meetings.GetMeetingInfo;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Meetings.GetMeetingsList;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Meetings.GetMeetingUsers;

namespace RU.NSU.FIT.VirtualMeetingManager.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MeetingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public MeetingsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Возвращает список всех мероприятий
    /// </summary>
    [HttpPost("getMeetingsList")]
    public Task<GetMeetingsListResponse> GetMeetingsList(GetMeetingsListQuery query)
    {
        return _mediator.Send(query, HttpContext.RequestAborted);
    }
    
    /// <summary>
    /// Возвращает список всех участников мероприятия
    /// </summary>
    [HttpPost("getMeetingUsers")]
    public Task<ICollection<UserDto>> GetMeetingUsers(GetMeetingUsersQuery query)
    {
        return _mediator.Send(query, HttpContext.RequestAborted);
    }
    
    /// <summary>
    /// Создает новое мероприятие
    /// </summary>
    [HttpPost("createMeeting")]
    public async Task<CreateMeetingResponse> CreateMeeting(CreateMeetingCommand command){
        return await _mediator.Send(command, HttpContext.RequestAborted);
    }
    
    /// <summary>
    /// Редактирует мероприятие
    /// </summary>
    [HttpPost("updateMeeting")]
    public async Task UpdateMeeting(UpdateMeetingCommand command){
        await _mediator.Send(command, HttpContext.RequestAborted);
    }
    
    /// <summary>
    /// Редактирует иконку мероприятия
    /// </summary>
    [HttpPost("updateMeetingImage")]
    public Task UpdateMeetingImage(UpdateMeetingImageCommand command)
    {
        return _mediator.Send(command, HttpContext.RequestAborted);
    }

    /// <summary>
    /// Возвращает иконку мероприятия
    /// </summary>
    [HttpPost("getMeetingImage")]
    public Task<GetMeetingImageResponse> GetMeetingImage(GetMeetingImageQuery query)
    {
        return _mediator.Send(query, HttpContext.RequestAborted);
    }
    
    /// <summary>
    /// Добавляет пользователя в мероприятие
    /// </summary>
    [HttpPost("visitMeeting")]
    public async Task VisitMeeting (VisitMeetingCommand command)
    {
        await _mediator.Send(command, HttpContext.RequestAborted);
    }
    
    /// <summary>
    /// Удаляет пользователя в мероприятие
    /// </summary>
    [HttpPost("cancelMeetingVisiting")]
    public async Task CancelMeetingVisiting (CancelMeetingVisitingCommand command)
    {
        await _mediator.Send(command, HttpContext.RequestAborted);
    }
    
    /// <summary>
    /// Возвращает информацию о мероприятии
    /// </summary>
    [HttpPost("getCurrentMeeting")]
    public Task<MeetingResponse> GetCurrentMeeting(GetMeetingInfoQuery query)
    {
        return _mediator.Send(query, HttpContext.RequestAborted);
    }
    
    /// <summary>
    /// Удаляет мероприятие
    /// </summary>
    [HttpPost("deleteMeeting")]
    public Task DeleteMeeting(DeleteMeetingCommand command)
    {
        return _mediator.Send(command, HttpContext.RequestAborted);
    }
    
}