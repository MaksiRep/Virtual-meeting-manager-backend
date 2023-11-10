using Microsoft.AspNetCore.Identity;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualManager.Domain.ValueTypes;

namespace RU.NSU.FIT.VirtualManager.Domain.Entities;

/// <summary>
/// Модель роли пользователя
/// </summary>
public class Role : IdentityRole<Guid>
{
    public RoleType Type => Name.MapToRoleType();
}