namespace RU.NSU.FIT.VirtualManager.Domain.Entities;

public class RefreshToken
{
    /// <summary>
    /// Идентификатор токена
    /// </summary>
    public Guid Id { get; private set; }
    
    /// <summary>
    /// Владелец токена
    /// </summary>
    public User Owner { get; private set; }

    /// <summary>
    /// Дата выдачи токена
    /// </summary>
    public DateTime IssuedAt { get; private set;}
    
    /// <summary>
    /// Дата окончания действия токена
    /// </summary>
    public DateTime ValidUntil { get; private set; }

    protected RefreshToken()
    {
        // Constructor for EF 
    }

    public RefreshToken(User owner, DateTime issuedAt, DateTime validUntil)
    {
        Owner = owner;
        IssuedAt = issuedAt;
        ValidUntil = validUntil;
    }
}