namespace RU.NSU.FIT.VirtualMeetingManager.Application.Settings;

public sealed record FrontendSettings
{
    /// <summary>
    /// URL страницы ввода нового пароля после сброса
    /// </summary>
    public string ResetPassUrl { get; init; }
    
    /// <summary>
    /// URL страницы восстановления пароля
    /// </summary>
    public string ForgotPassUrl { get; init; }
}