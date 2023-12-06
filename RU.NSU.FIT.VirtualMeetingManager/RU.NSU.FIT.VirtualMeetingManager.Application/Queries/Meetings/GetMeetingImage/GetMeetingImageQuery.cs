using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualManager.Domain.Exceptions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Meetings.GetMeetingImage;

public record GetMeetingImageQuery : IRequest<GetMeetingImageResponse>
{
    public int MeetingId { get; init; }
    
    [UsedImplicitly]
    public sealed class GetMeetingImageQueryHandler : IRequestHandler<GetMeetingImageQuery, GetMeetingImageResponse>
    {
        private readonly IVMMDbContext _vmmDbContext;

        public GetMeetingImageQueryHandler(IVMMDbContext vmmDbContext)
        {
            _vmmDbContext = vmmDbContext;
        }

        public async Task<GetMeetingImageResponse> Handle(GetMeetingImageQuery request, CancellationToken cancellationToken)
        {
            var imageUrl = await _vmmDbContext.Meetings
                .Where(m => m.Id == request.MeetingId)
                .Select(m => m.ImageUrl)
                .FirstOrDefaultAsync(cancellationToken);
            
            EntityNotFoundException.ThrowIfNull(imageUrl, "Не найдено мероприятие с Id={0}", request.MeetingId);

            return new GetMeetingImageResponse
            {
                ImageUrl = imageUrl
            };
        }
    }
}