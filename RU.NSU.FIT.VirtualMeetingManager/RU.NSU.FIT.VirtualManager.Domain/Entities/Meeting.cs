namespace RU.NSU.FIT.VirtualManager.Domain.Entities;

public class Meeting
{
    public Meeting()
    {
    }

    public Meeting(string name, string description, DateTime startDate, DateTime endDate, string image, int maxUsers, int minAge, string sex)
    {
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Image = image;
        MaxUsers = maxUsers;
        MinAge = minAge;
        Sex = sex;
    }

    public int Id { get; private set; }
    
    public string Name { get; private set; }
    
    public string Description { get; private set; }
    
    public DateTime StartDate { get; private set; }
    
    public DateTime EndDate { get; private set; }
    
    public string Image { get; private set; }
    
    public int MaxUsers { get; private set; }
    
    public int MinAge { get; private set; }
    
    public string Sex { get; private set; }
    
    public User Manager { get; private set; }
    public IList<User> Users { get; private set; } = new List<User>();
}