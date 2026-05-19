using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HogwartsWebsite.Filters;

public class AdminAuthFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.Session.GetString("AdminLoggedIn") != "true")
            context.Result = new RedirectToActionResult("Login", "AdminAccount", null);
    }
}

public class AdminAuthAttribute : TypeFilterAttribute
{
    public AdminAuthAttribute() : base(typeof(AdminAuthFilter)) { }
}
