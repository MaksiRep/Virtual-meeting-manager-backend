namespace RU.NSU.FIT.VirtualManager.Domain.Constants;

public static class EntityConstants
{
    public static class Meeting
    {
        public static readonly Size Name = new(3, 128);
        public static readonly Size Description = new(10, 1024);
        public static readonly Size ShortDescription = new(1, 300);
    }
    
    public static class User
    {
        public static readonly Size Email = new()
    }

    public record Size(int Min, int Max)
    {
        public Size(int exact) : this(exact, exact) {}
    }
}