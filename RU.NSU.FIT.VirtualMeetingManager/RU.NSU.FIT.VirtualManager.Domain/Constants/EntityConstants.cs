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
        public static readonly Size Age = new(14, 200);
        public static readonly Size FirstName = new(1, 128);
        public static readonly Size LastName = new(1, 128);
        public static readonly Size Email = new(3, 128);
        public static readonly Size PhoneNumber = new(1, 30);
        public static readonly Size Password = new(1, 30);
        public static readonly DateOnly BirthDateMin = new(1900, 1, 1);
    }

    public record Size(int Min, int Max)
    {
        public Size(int exact) : this(exact, exact) {}
    }
}