using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HogwartsWebsite.Data;

namespace HogwartsWebsite.Controllers;

public class StaffController : Controller
{
    private readonly AppDbContext _db;

    public StaffController(AppDbContext db) => _db = db;

    // Fetches all staff; headmaster separated in view via IsHeadmaster flag
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Academic Staff";

        var staff = await _db.Staff
            .OrderByDescending(s => s.IsHeadmaster)
            .ThenBy(s => s.FullName)
            .ToListAsync();

        return View(staff);
    }
}
