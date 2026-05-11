using System.ComponentModel.DataAnnotations;

namespace HogwartsWebsite.Models;

public class SchoolEvent
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Location { get; set; } = string.Empty;

    public DateTime EventDate { get; set; }
}
