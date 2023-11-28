namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Base;

public enum RoleType
{
    /// <summary>
    /// Роль не установлена
    /// </summary>
    Undefined = 0,
    
    /// <summary>
    /// Обычный пользователь
    /// </summary>
    User = 1,
    
    /// <summary>
    /// Администратор
    /// </summary>
    Admin = 2,
    
    /// <summary>
    /// Главный Администратор
    /// </summary>
    MainAdmin = 3
}