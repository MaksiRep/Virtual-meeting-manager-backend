using Microsoft.AspNetCore.Identity;

namespace RU.NSU.FIT.VirtualManager.Domain.Entities;

/// <summary>
/// Модель пользователя
/// </summary>
public class User : IdentityUser<Guid>
{
    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    /// Email
    /// </summary>
    public override string Email { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateOnly BirthDate { get; private set; }
    
    public GenderType Gender { get; private set; }

    public DateTime RegisteredOn { get; private set; }

    /// <summary>
    /// Роли пользователя
    /// </summary>
    public IList<Role> Roles { get; private set; } = new List<Role>();
    
    public IList<Meeting> Meetings { get; private set; } = new List<Meeting>();
    
    public IList<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();

    public void AddRefreshToken(RefreshToken refreshToken)
    {
        RefreshTokens.Add(refreshToken);
    }

    #region Constructors

    protected User()
    {
        // Конструктор для EF
    }

    public User(
        string firstName, string lastName,
        string email, DateOnly birthDate, DateTime registeredOn)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        BirthDate = birthDate;
        RegisteredOn = registeredOn;
    }

    #endregion
}