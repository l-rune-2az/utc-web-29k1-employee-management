using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Extensions;

namespace EmployeeManagement.Controllers;

public abstract class BaseController : Controller
{
    protected Guid? CurrentEmployeeId => User.GetEmployeeId();
    protected string CurrentUsername  => User.Identity?.Name ?? "system";
}
