using Microsoft.AspNetCore.Mvc;

namespace HogwartsWebsite.Controllers;

// Static page — no database queries
public class HousesController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Houses";
        return View();
    }
}
