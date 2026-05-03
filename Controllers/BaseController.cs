using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Extensions;

namespace EmployeeManagement.Controllers;

// BaseController tập trung logic dùng chung — các controller kế thừa từ đây
public abstract class BaseController : Controller
{
    protected Guid? CurrentEmployeeId => User.GetEmployeeId();
    protected string CurrentUsername  => User.Identity?.Name ?? "system";
}
