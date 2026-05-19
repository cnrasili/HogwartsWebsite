using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HogwartsWebsite.Data;
using HogwartsWebsite.Filters;
using HogwartsWebsite.Models;

namespace HogwartsWebsite.Controllers;

[Route("admin/announcements")]
[AdminAuth]
public class AdminAnnouncementsController : Controller
{
    private readonly AppDbContext _db;

    public AdminAnnouncementsController(AppDbContext db) => _db = db;

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Announcements";
        var items = await _db.Announcements.OrderByDescending(a => a.PublishedDate).ToListAsync();
        return View(items);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        ViewData["Title"] = "New Announcement";
        return View(new Announcement { PublishedDate = DateTime.Today });
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Announcement model)
    {
        if (!ModelState.IsValid) { ViewData["Title"] = "New Announcement"; return View(model); }
        _db.Announcements.Add(model);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet("edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var item = await _db.Announcements.FindAsync(id);
        if (item == null) return NotFound();
        ViewData["Title"] = "Edit Announcement";
        return View(item);
    }

    [HttpPost("edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Announcement model)
    {
        if (id != model.Id) return BadRequest();
        if (!ModelState.IsValid) { ViewData["Title"] = "Edit Announcement"; return View(model); }
        _db.Announcements.Update(model);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.Announcements.FindAsync(id);
        if (item == null) return NotFound();
        ViewData["Title"] = "Delete Announcement";
        return View(item);
    }

    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var item = await _db.Announcements.FindAsync(id);
        if (item != null) { _db.Announcements.Remove(item); await _db.SaveChangesAsync(); }
        return RedirectToAction("Index");
    }
}
