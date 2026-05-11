using System.ComponentModel.DataAnnotations;

namespace HogwartsWebsite.Models;

public class StaffMember
{
    public int Id { get; set; }

    [Required]
    public string FullName { get; set; } = string.Empty;

    // "Professor" / "Madam" / etc.
    public string Title { get; set; } = string.Empty;

    // Nullable for non-teaching roles
    public string? Subject { get; set; }

    [Required]
    public string Bio { get; set; } = string.Empty;

    // Filename only, e.g. "Severus-Snape.webp"
    public string? PhotoPath { get; set; }

    // Pins to top of staff page
    public bool IsHeadmaster { get; set; }
}
