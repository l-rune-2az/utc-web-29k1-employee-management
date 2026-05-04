using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EmployeeManagement.Controllers;

public abstract class BaseController : Controller
{
    protected Guid? CurrentEmployeeId
    {
        get
        {
            string? value = User.FindFirst("EmployeeId")?.Value;
            return Guid.TryParse(value, out Guid id) ? id : null;
        }
    }

    protected string CurrentUsername => User.Identity?.Name ?? "system";

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
        Response.Headers["Pragma"]        = "no-cache";
        base.OnActionExecuting(context);
    }
}
