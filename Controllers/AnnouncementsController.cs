using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HogwartsWebsite.Data;

namespace HogwartsWebsite.Controllers;

public class AnnouncementsController : Controller
{
    private readonly AppDbContext _db;

    public AnnouncementsController(AppDbContext db) => _db = db;

    // Fetches all announcements newest first; top 6 shown as notices, rest as archive
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Announcements";

        var announcements = await _db.Announcements
            .OrderByDescending(a => a.PublishedDate)
            .ToListAsync();

        return View(announcements);
    }
}
