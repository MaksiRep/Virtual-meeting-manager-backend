using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualManager.Domain.ValueTypes;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Extensions;

public static class AuthorizationExtensions
{
    public static bool CanCreateMeeting(this ICurrentUser user)
    {
        return user.ContainRoles(RoleType.User);
    }
    
    public static bool CanEditAllMeetings(this ICurrentUser user)
    {
        return user.ContainRoles(RoleType.Admin, RoleType.MainAdmin);
    }
    
    public static bool CanDeleteAllMeetings(this ICurrentUser user)
    {
        return user.ContainRoles(RoleType.Admin, RoleType.MainAdmin);
    }
    
    public static bool CanGetAllUsersDetails(this ICurrentUser user)
    {
        return user.ContainRoles(RoleType.Admin, RoleType.MainAdmin);
    }
    
    public static bool CanGetAllUsersList(this ICurrentUser user)
    {
        return user.ContainRoles(RoleType.Admin, RoleType.MainAdmin);
    }
    
    public static bool CanEditAllUsersDetails(this ICurrentUser user)
    {
        return user.ContainRoles(RoleType.Admin, RoleType.MainAdmin);
    }
    
    public static bool CanEditUsersRoles(this ICurrentUser user)
    {
        return user.ContainRoles(RoleType.MainAdmin);
    }

    public static bool ContainRoles(this ICurrentUser currentUser, params RoleType[] roles)
    {
        return currentUser.Roles.Intersect(roles).Any();
    }

    public static Queries.Base.RoleType ToOuterType(this RoleType innerType)
    {
        return innerType switch
        {
            RoleType.Undefined => Queries.Base.RoleType.Undefined,
            RoleType.User => Queries.Base.RoleType.User,
            RoleType.Admin => Queries.Base.RoleType.Admin,
            RoleType.MainAdmin => Queries.Base.RoleType.MainAdmin,
            _ => throw new ArgumentOutOfRangeException(nameof(innerType), innerType, null)
        };
    }

    public static RoleType ToInnerType(this Queries.Base.RoleType outerType)
    {
        return outerType switch
        {
            Queries.Base.RoleType.Undefined => RoleType.Undefined,
            Queries.Base.RoleType.User => RoleType.User,
            Queries.Base.RoleType.Admin => RoleType.Admin,
            Queries.Base.RoleType.MainAdmin => RoleType.MainAdmin,
            _ => throw new ArgumentOutOfRangeException(nameof(outerType), outerType, null)
        };
    }
}