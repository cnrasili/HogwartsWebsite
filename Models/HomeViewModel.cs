namespace HogwartsWebsite.Models;

// View model for the home page — latest announcements and upcoming events
public class HomeViewModel
{
    public List<Announcement> LatestAnnouncements { get; set; } = new();
    public List<SchoolEvent> UpcomingEvents { get; set; } = new();
}
