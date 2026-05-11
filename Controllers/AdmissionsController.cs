using Microsoft.AspNetCore.Mvc;

namespace HogwartsWebsite.Controllers;

// Static page — no database queries
public class AdmissionsController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Admissions";
        return View();
    }
}
