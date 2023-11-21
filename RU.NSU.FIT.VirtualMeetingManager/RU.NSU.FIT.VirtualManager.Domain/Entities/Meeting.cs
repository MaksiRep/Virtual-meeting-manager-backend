namespace RU.NSU.FIT.VirtualManager.Domain.Entities;

public class Meeting
{
    public Meeting()
    {
    }

    public Meeting(string name, string description, DateTime startDate, DateTime endDate, string imageUrl, int maxUsers, int minAge, GenderType gender)
    {
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        ImageUrl = imageUrl;
        MaxUsers = maxUsers;
        MinAge = minAge;
        Gender = gender;
    }

    public int Id { get; private set; }
    
    public string Name { get; private set; }
    
    public string Description { get; private set; }
    
    public DateTime StartDate { get; private set; }
    
    public DateTime EndDate { get; private set; }
    
    public string ImageUrl { get; private set; }
    
    public int MaxUsers { get; private set; }
    
    public int MinAge { get; private set; }
    
    public GenderType Gender { get; private set; }
    
    public User Manager { get; private set; }
    public IList<User> Users { get; private set; } = new List<User>();
}