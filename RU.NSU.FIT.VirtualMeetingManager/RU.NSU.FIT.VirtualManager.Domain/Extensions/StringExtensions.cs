using System.Diagnostics.CodeAnalysis;

namespace RU.NSU.FIT.VirtualManager.Domain.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? str)
    {
        return string.IsNullOrEmpty(str);
    }
}