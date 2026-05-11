using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HogwartsWebsite.Data;

namespace HogwartsWebsite.Controllers;

public class CoursesController : Controller
{
    private readonly AppDbContext _db;

    public CoursesController(AppDbContext db) => _db = db;

    // Fetches all courses; grouped by Category in view
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Courses";

        var courses = await _db.Courses
            .OrderBy(c => c.Category)
            .ThenBy(c => c.Name)
            .ToListAsync();

        var staffList = await _db.Staff
            .Where(s => s.Subject != null)
            .ToListAsync();

        var instructors = new Dictionary<string, string>();
        foreach (var course in courses)
        {
            var match = staffList.FirstOrDefault(s =>
                s.Subject!.Contains(course.Name, StringComparison.OrdinalIgnoreCase) ||
                course.Name.Contains(s.Subject!, StringComparison.OrdinalIgnoreCase));
            if (match != null)
                instructors[course.Name] = $"{match.Title} {match.FullName}";
        }
        ViewData["Instructors"] = instructors;

        return View(courses);
    }
}
