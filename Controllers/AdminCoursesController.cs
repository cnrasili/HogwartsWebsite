using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HogwartsWebsite.Data;
using HogwartsWebsite.Filters;
using HogwartsWebsite.Models;

namespace HogwartsWebsite.Controllers;

[Route("admin/courses")]
[AdminAuth]
public class AdminCoursesController : Controller
{
    private readonly AppDbContext _db;

    public AdminCoursesController(AppDbContext db) => _db = db;

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Courses";
        var items = await _db.Courses.OrderBy(c => c.Category).ThenBy(c => c.Name).ToListAsync();
        return View(items);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        ViewData["Title"] = "New Course";
        return View(new Course());
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Course model)
    {
        if (!ModelState.IsValid) { ViewData["Title"] = "New Course"; return View(model); }
        _db.Courses.Add(model);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet("edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var item = await _db.Courses.FindAsync(id);
        if (item == null) return NotFound();
        ViewData["Title"] = "Edit Course";
        return View(item);
    }

    [HttpPost("edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Course model)
    {
        if (id != model.Id) return BadRequest();
        if (!ModelState.IsValid) { ViewData["Title"] = "Edit Course"; return View(model); }
        _db.Courses.Update(model);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.Courses.FindAsync(id);
        if (item == null) return NotFound();
        ViewData["Title"] = "Delete Course";
        return View(item);
    }

    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var item = await _db.Courses.FindAsync(id);
        if (item != null) { _db.Courses.Remove(item); await _db.SaveChangesAsync(); }
        return RedirectToAction("Index");
    }
}
