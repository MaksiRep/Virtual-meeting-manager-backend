using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Base;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Users.GetUserInfo;

public record GetUserInfoResponse
{
    public Guid Id { get; init; }
    
    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; init; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; init; }

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; init; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateOnly BirthDate { get; init; }

    /// <summary>
    /// Пол
    /// </summary>
    public GenderType Gender { get; init; }

    /// <summary>
    /// Дата регистрации
    /// </summary>
    public DateTime RegisteredOn { get; init; }

    /// <summary>
    /// Роли пользователя
    /// </summary>
    public IList<RoleType> Roles { get; init; } = Array.Empty<RoleType>();
}