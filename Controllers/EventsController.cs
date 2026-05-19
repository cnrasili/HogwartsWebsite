using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HogwartsWebsite.Data;

namespace HogwartsWebsite.Controllers;

public class EventsController : Controller
{
    private readonly AppDbContext _db;

    public EventsController(AppDbContext db) => _db = db;

    // Splits events into upcoming and past relative to today
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Events";

        var events = await _db.Events
            .OrderBy(e => e.EventDate)
            .ToListAsync();

        ViewData["Today"] = DateTime.Today;

        return View(events);
    }
}
