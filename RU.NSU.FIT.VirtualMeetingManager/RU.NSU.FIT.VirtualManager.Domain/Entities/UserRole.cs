using Microsoft.AspNetCore.Identity;

namespace RU.NSU.FIT.VirtualManager.Domain.Entities;

public class UserRole : IdentityUserRole<Guid>
{
    public UserRole()
    {
        // Конструктор для EF
    }
    
    public UserRole(Guid userId, Guid roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }
}