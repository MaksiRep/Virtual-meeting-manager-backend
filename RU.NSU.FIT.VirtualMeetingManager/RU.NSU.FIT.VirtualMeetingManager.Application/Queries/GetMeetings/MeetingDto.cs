namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.GetMeetings;

public record MeetingDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Image { get; set; }
}