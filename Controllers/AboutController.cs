using Microsoft.AspNetCore.Mvc;

namespace HogwartsWebsite.Controllers;

// Static page — no database queries
public class AboutController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "About & History";
        return View();
    }
}
