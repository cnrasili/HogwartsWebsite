using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HogwartsWebsite.Data;

namespace HogwartsWebsite.Controllers;

public class EventsController : Controller
{
    private readonly AppDbContext _db;

    public EventsController(AppDbContext db) => _db = db;

    // Fetches all events ordered by date ascending
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Events";

        var events = await _db.Events
            .OrderBy(e => e.EventDate)
            .ToListAsync();

        return View(events);
    }
}
