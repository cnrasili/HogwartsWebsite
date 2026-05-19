using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HogwartsWebsite.Data;
using HogwartsWebsite.Filters;

namespace HogwartsWebsite.Controllers;

[Route("admin")]
[AdminAuth]
public class AdminController : Controller
{
    private readonly AppDbContext _db;

    public AdminController(AppDbContext db) => _db = db;

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Dashboard";
        ViewData["AnnouncementCount"] = await _db.Announcements.CountAsync();
        ViewData["EventCount"] = await _db.Events.CountAsync();
        ViewData["StaffCount"] = await _db.Staff.CountAsync();
        ViewData["CourseCount"] = await _db.Courses.CountAsync();
        return View();
    }
}
