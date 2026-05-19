using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HogwartsWebsite.Data;
using HogwartsWebsite.Filters;
using HogwartsWebsite.Models;

namespace HogwartsWebsite.Controllers;

[Route("admin/staff")]
[AdminAuth]
public class AdminStaffController : Controller
{
    private readonly AppDbContext _db;
    private readonly IWebHostEnvironment _env;

    public AdminStaffController(AppDbContext db, IWebHostEnvironment env)
    {
        _db = db;
        _env = env;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Staff";
        var items = await _db.Staff.OrderByDescending(s => s.IsHeadmaster).ThenBy(s => s.FullName).ToListAsync();
        return View(items);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        ViewData["Title"] = "New Staff Member";
        return View(new StaffMember());
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(StaffMember model, IFormFile? photo)
    {
        if (photo != null && photo.Length > 0)
            model.PhotoPath = await SavePhoto(photo);

        if (!ModelState.IsValid) { ViewData["Title"] = "New Staff Member"; return View(model); }
        _db.Staff.Add(model);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet("edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var item = await _db.Staff.FindAsync(id);
        if (item == null) return NotFound();
        ViewData["Title"] = "Edit Staff Member";
        return View(item);
    }

    [HttpPost("edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, StaffMember model, IFormFile? photo)
    {
        if (id != model.Id) return BadRequest();

        if (photo != null && photo.Length > 0)
            model.PhotoPath = await SavePhoto(photo);

        if (!ModelState.IsValid) { ViewData["Title"] = "Edit Staff Member"; return View(model); }
        _db.Staff.Update(model);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.Staff.FindAsync(id);
        if (item == null) return NotFound();
        ViewData["Title"] = "Delete Staff Member";
        return View(item);
    }

    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var item = await _db.Staff.FindAsync(id);
        if (item != null) { _db.Staff.Remove(item); await _db.SaveChangesAsync(); }
        return RedirectToAction("Index");
    }

    // Saves uploaded photo to wwwroot/assets/images and returns filename
    private async Task<string> SavePhoto(IFormFile photo)
    {
        var fileName = Path.GetFileName(photo.FileName);
        var filePath = Path.Combine(_env.WebRootPath, "assets", "images", fileName);
        await using var stream = System.IO.File.Create(filePath);
        await photo.CopyToAsync(stream);
        return fileName;
    }
}
