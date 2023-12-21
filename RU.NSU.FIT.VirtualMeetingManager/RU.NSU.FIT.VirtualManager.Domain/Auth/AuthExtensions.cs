using RU.NSU.FIT.VirtualManager.Domain.ValueTypes;

namespace RU.NSU.FIT.VirtualManager.Domain.Auth;

public static class AuthExtensions
{
    public static RoleType MapToRoleType(this string? roleName)
    {
        return roleName switch
        {
            RoleConstants.User => RoleType.User,
            RoleConstants.Admin => RoleType.Admin,
            RoleConstants.MainAdmin => RoleType.MainAdmin,
            _ => RoleType.Undefined
        };
    }

    public static string? GetRoleName(this RoleType roleType)
    {
        return roleType switch
        {
            RoleType.User => RoleConstants.User,
            RoleType.Admin => RoleConstants.Admin,
            RoleType.MainAdmin => RoleConstants.MainAdmin,
            _ => null
        };
    }
}