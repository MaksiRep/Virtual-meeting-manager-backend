using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualManager.Domain.Exceptions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Extensions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Base;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Users.GetUserInfo;

public record GetUserInfoQuery : IRequest<GetUserInfoResponse>
{
    public Guid UserId { get; init; }
    
    [UsedImplicitly]
    public sealed class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery,GetUserInfoResponse>
    {
        private readonly IVMMDbContext _vmmDbContext;
        private readonly ICurrentUser _currentUser;

        public GetUserInfoQueryHandler(IVMMDbContext vmmDbContext, ICurrentUser currentUser)
        {
            _vmmDbContext = vmmDbContext;
            _currentUser = currentUser;
        }

        public async Task<GetUserInfoResponse> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            if (request.UserId != _currentUser.Id && _currentUser.CanGetAllUsersDetails() is false)
            {
                throw new ForbiddenException(
                    "Данный пользователь не имеет прав не просмотр информации о другом пользователе");
            }
            
            var user = await _vmmDbContext.Users
                .AsNoTracking()
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            EntityNotFoundException.ThrowIfNull(user, "Не найден юзер с Id={0}", request.UserId);

            return new GetUserInfoResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                BirthDate = user.BirthDate,
                Gender = (GenderType) user.Gender,
                RegisteredOn = user.RegisteredOn,
                Roles = user.Roles.Select(r => r.Type.ToOuterType()).ToList()
            };
        }
    }
}