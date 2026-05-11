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

        return View(courses);
    }
}
