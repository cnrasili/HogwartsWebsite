using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HogwartsWebsite.Data;
using HogwartsWebsite.Filters;
using HogwartsWebsite.Models;

namespace HogwartsWebsite.Controllers;

[Route("admin/events")]
[AdminAuth]
public class AdminEventsController : Controller
{
    private readonly AppDbContext _db;

    public AdminEventsController(AppDbContext db) => _db = db;

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Events";
        var items = await _db.Events.OrderBy(e => e.EventDate).ToListAsync();
        return View(items);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        ViewData["Title"] = "New Event";
        return View(new SchoolEvent { EventDate = DateTime.Today });
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SchoolEvent model)
    {
        if (!ModelState.IsValid) { ViewData["Title"] = "New Event"; return View(model); }
        _db.Events.Add(model);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet("edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var item = await _db.Events.FindAsync(id);
        if (item == null) return NotFound();
        ViewData["Title"] = "Edit Event";
        return View(item);
    }

    [HttpPost("edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, SchoolEvent model)
    {
        if (id != model.Id) return BadRequest();
        if (!ModelState.IsValid) { ViewData["Title"] = "Edit Event"; return View(model); }
        _db.Events.Update(model);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.Events.FindAsync(id);
        if (item == null) return NotFound();
        ViewData["Title"] = "Delete Event";
        return View(item);
    }

    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var item = await _db.Events.FindAsync(id);
        if (item != null) { _db.Events.Remove(item); await _db.SaveChangesAsync(); }
        return RedirectToAction("Index");
    }
}
