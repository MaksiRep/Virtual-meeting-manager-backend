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
    
    public static IQueryable<User> OrderByDefault(this IQueryable<User> query)
    {
        return query
            .OrderBy(u => u.LastName)
            .ThenBy(u => u.FirstName)
            // .ThenByDescending(u => u.BirthDate)
            .ThenBy(u => u.Id);
    }
}