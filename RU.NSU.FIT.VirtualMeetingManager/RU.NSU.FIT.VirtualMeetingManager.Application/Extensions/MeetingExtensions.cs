using RU.NSU.FIT.VirtualManager.Domain.Entities;
using RU.NSU.FIT.VirtualManager.Domain.Extensions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Base;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Extensions;

public static class MeetingExtensions
{
    public static IQueryable<Meeting> OrderByDefault(this IQueryable<Meeting> query, Guid userId, DateTime now)
    {
        return query
            .OrderBy(m => m.EndDate < now)
            .ThenByDescending(m => m.Users.Any(u => u.Id == userId) || m.Manager.Id == userId)
            .ThenBy(m => m.StartDate)
            .ThenBy(m => m.EndDate)
            .ThenBy(m => m.Id);
    }

    public static IQueryable<Meeting> FilterByDates(this IQueryable<Meeting> query, DateTime? startDate,
        DateTime? endDate)
    {
        if (startDate is not null)
        {
            query = query.Where(m => m.StartDate >= startDate);
        }

        if (endDate is not null)
        {
            query = query.Where(m => m.EndDate <= endDate);
        }

        return query;
    }

    public static IQueryable<Meeting> FilterByGender(this IQueryable<Meeting> query, GenderType? genderType)
    {
        if (genderType is null)
        {
            return query;
        }

        var dbType = (VirtualManager.Domain.ValueTypes.GenderType)genderType;

        return query.Where(m => m.Gender == dbType);
    }
    
    
    public static IQueryable<Meeting> FilterByUserPresence(this IQueryable<Meeting> query, bool? isUserPresent, Guid userId)
    {
        if (isUserPresent is null)
        {
            return query;
        }

        return query.Where(m => m.Users.Any(u => u.Id == userId) || m.Manager.Id == userId);
    }

    public static IQueryable<Meeting> FilterByMinAge(this IQueryable<Meeting> query, int? minAge)
    {
        if (minAge is null)
        {
            return query;
        }

        return query.Where(m => m.MinAge >= minAge);
    }

    public static IQueryable<Meeting> FilterByName(this IQueryable<Meeting> query, string? name)
    {
        if (name.IsNullOrEmpty())
        {
            return query;
        }

        return query.Where(m => m.Name.ToLower().Contains(name.ToLower()));
    }

    public static IQueryable<Meeting> FilterByUserAge(this IQueryable<Meeting> query, int? userAge)
    {
        if (userAge is null)
        {
            return query;
        }

        return query.Where(m => m.MinAge == null || userAge >= m.MinAge.Value);
    }

    public static IQueryable<Meeting> FilterById(this IQueryable<Meeting> query, int? meetingId)
    {
        if (meetingId is null)
        {
            return query;
        }

        return query.Where(m => m.Id == meetingId);
    }
}