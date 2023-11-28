using RU.NSU.FIT.VirtualManager.Domain.Entities;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Extensions;

public static class UsersExtensions
{
    public static IQueryable<User> FilterByEmail(this IQueryable<User> query, string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return query;
        }

        return query.Where(u => u.Email == email);
    }
}