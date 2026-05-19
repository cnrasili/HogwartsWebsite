using Microsoft.AspNetCore.Mvc;

namespace HogwartsWebsite.Controllers;

[Route("admin")]
public class AdminAccountController : Controller
{
    private readonly IConfiguration _config;

    public AdminAccountController(IConfiguration config) => _config = config;

    [HttpGet("login")]
    public IActionResult Login()
    {
        if (HttpContext.Session.GetString("AdminLoggedIn") == "true")
            return Redirect("/admin");

        ViewData["Title"] = "Admin Login";
        return View();
    }

    [HttpPost("login")]
    [ValidateAntiForgeryToken]
    public IActionResult Login(string username, string password)
    {
        if (username == _config["Admin:Username"] && password == _config["Admin:Password"])
        {
            HttpContext.Session.SetString("AdminLoggedIn", "true");
            return Redirect("/admin");
        }

        ViewData["Title"] = "Admin Login";
        ViewData["Error"] = "Invalid username or password.";
        return View();
    }

    [HttpPost("logout")]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
