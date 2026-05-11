using System.ComponentModel.DataAnnotations;

namespace HogwartsWebsite.Models;

public class Announcement
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Body { get; set; } = string.Empty;

    // "Academic" / "Events" / "Safety" / "Sports" / "Notice"
    public string Category { get; set; } = string.Empty;

    public DateTime PublishedDate { get; set; }
}
