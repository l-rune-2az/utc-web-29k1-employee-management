using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models.Entities;
using EmployeeManagement.Services;

namespace EmployeeManagement.Controllers;

// Trang hồ sơ cá nhân — chỉ EMPLOYEE mới vào đây
[Authorize(Roles = "EMPLOYEE")]
public class ProfileController : BaseController
{
    private readonly IEmployeeService _empService;
    private readonly IContractService _contractService;
    private readonly IEmployeeAllowanceService _allowanceService;
    private readonly IDependentService _dependentService;

    public ProfileController(
        IEmployeeService empService,
        IContractService contractService,
        IEmployeeAllowanceService allowanceService,
        IDependentService dependentService)
    {
        _empService = empService;
        _contractService = contractService;
        _allowanceService = allowanceService;
        _dependentService = dependentService;
    }

    // GET: /Profile — Xem thông tin cá nhân
    public async Task<IActionResult> Index()
    {
        if (CurrentEmployeeId == null)
            return RedirectToAction("Login", "Account");

        Employee? employee = await _empService.GetByIdAsync(CurrentEmployeeId.Value);
        if (employee == null) return NotFound();

        ViewBag.Contracts = await _contractService.GetByEmployeeIdAsync(CurrentEmployeeId.Value);
        ViewBag.Allowances = await _allowanceService.GetByEmployeeIdAsync(CurrentEmployeeId.Value);
        ViewBag.Dependents = await _dependentService.GetByEmployeeIdAsync(CurrentEmployeeId.Value);

        return View(employee);
    }
}
