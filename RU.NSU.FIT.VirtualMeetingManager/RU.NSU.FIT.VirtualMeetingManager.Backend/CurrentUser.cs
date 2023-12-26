using System.Security.Claims;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualManager.Domain.Exceptions;
using RU.NSU.FIT.VirtualManager.Domain.ValueTypes;

namespace RU.NSU.FIT.VirtualMeetingManager.Backend;

public class CurrentUser : ICurrentUser
{
    public Guid Id { get; }
    public string Name { get; }
    public IList<RoleType> Roles { get; }

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        var context = httpContextAccessor.HttpContext
            ?? throw new BadRequestException($"{nameof(IHttpContextAccessor)} не задан");

        if (context.User.Identity is not ClaimsIdentity identity)
        {
            throw new BadRequestException($"{nameof(ClaimsIdentity)} не задана");
        }

        Id = identity.FindFirst(InnerClaimTypes.UserId) is { } id
            ? Guid.Parse(id.Value)
            : throw MakeClaimNotFoundException(InnerClaimTypes.UserId);
        Name = identity.FindFirst(InnerClaimTypes.UserName) is { } name
            ? name.Value
            : throw MakeClaimNotFoundException(InnerClaimTypes.UserName);
        Roles = identity.FindAll(InnerClaimTypes.UserRole)
            .Select(r => r.Value.MapToRoleType())
            .Distinct()
            .ToArray();
    }


    private static Exception MakeClaimNotFoundException(string claimType)
    {
        return new BadRequestException($"Claim {claimType} не найден");
    }
}