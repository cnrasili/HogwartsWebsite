using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HogwartsWebsite.Data;
using HogwartsWebsite.Models;

namespace HogwartsWebsite.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _db;

    public HomeController(AppDbContext db) => _db = db;

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        ViewData["Title"] = "Error";
        return View();
    }

    // Fetches 3 latest announcements + 3 upcoming events for home page
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Home";

        var announcements = await _db.Announcements
            .OrderByDescending(a => a.PublishedDate)
            .Take(3)
            .ToListAsync();

        var events = await _db.Events
            .Where(e => e.EventDate >= DateTime.Today)
            .OrderBy(e => e.EventDate)
            .Take(3)
            .ToListAsync();

        var vm = new HomeViewModel
        {
            LatestAnnouncements = announcements,
            UpcomingEvents = events
        };

        return View(vm);
    }
}
