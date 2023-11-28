using JetBrains.Annotations;
using MediatR;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Base;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Users.GetCurrentUser;

public record GetCurrentUserQuery : IRequest<CurrentUserDto>
{
    [UsedImplicitly]
    public sealed class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, CurrentUserDto>
    {
        private readonly ICurrentUser _currentUser;

        public GetCurrentUserQueryHandler(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public Task<CurrentUserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new CurrentUserDto
            {
                Id = _currentUser.Id,
                Name = _currentUser.Name,
                Roles = _currentUser.Roles.Select(r => (RoleType) r).ToList()
            });
        }
    }
}