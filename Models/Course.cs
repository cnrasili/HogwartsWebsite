using System.ComponentModel.DataAnnotations;

namespace HogwartsWebsite.Models;

public class Course
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    // e.g. "Years 1–7"
    public string YearLevels { get; set; } = string.Empty;

    // "Core" / "Elective" / "Advanced Elective" / "Other" / "Extra-Curricular"
    public string Category { get; set; } = string.Empty;
}
