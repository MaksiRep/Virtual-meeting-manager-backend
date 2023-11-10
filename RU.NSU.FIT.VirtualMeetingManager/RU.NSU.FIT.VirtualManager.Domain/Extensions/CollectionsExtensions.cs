using System.Diagnostics.CodeAnalysis;

namespace RU.NSU.FIT.VirtualManager.Domain.Extensions;

public static class CollectionsExtensions
{
    public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this ICollection<T>? collection)
    {
        return collection is null or {Count: 0};
    }
}