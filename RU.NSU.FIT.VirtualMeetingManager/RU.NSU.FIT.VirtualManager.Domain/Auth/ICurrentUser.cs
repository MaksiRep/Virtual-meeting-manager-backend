using RU.NSU.FIT.VirtualManager.Domain.ValueTypes;

namespace RU.NSU.FIT.VirtualManager.Domain.Auth;

public interface ICurrentUser
{
    Guid Id { get; }
    string Name { get; }
    IList<RoleType> Roles { get; }
}