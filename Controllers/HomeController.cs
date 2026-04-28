using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers;

[Authorize]
public class HomeController : BaseController
{
    public IActionResult Index() => View();
}
