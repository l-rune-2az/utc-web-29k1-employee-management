using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.Enums;
using EmployeeManagement.Models.ViewModels;
using EmployeeManagement.Services;

namespace EmployeeManagement.Controllers;

[Authorize]
public class DependentController : BaseController
{
    private readonly IDependentService _service;
    private readonly IEmployeeService _empService;

    public DependentController(IDependentService service, IEmployeeService empService)
    {
        _service = service;
        _empService = empService;
    }

    [HttpGet]
    [Authorize(Roles = "HR_MANAGER")]
    public async Task<IActionResult> Edit(Guid id)
    {
        EmployeeDependent? dep = await _service.GetByIdAsync(id);
        if (dep == null) return NotFound();
        DependentFormViewModel vm = new DependentFormViewModel
        {
            Id = dep.Id, EmpId = dep.EmpId, FullName = dep.FullName,
            Relationship = dep.Relationship ?? "CHILD",
            DateOfBirth = dep.DateOfBirth, NationalId = dep.NationalId
        };
        await PopulateEmployeeOptions(vm);
        return View(vm);
    }

    public async Task<IActionResult> Index(Guid? empId)
    {
        if (User.IsInRole("HR_MANAGER"))
        {
            List<EmployeeDependent> dependents = empId.HasValue
                ? await _service.GetByEmployeeIdAsync(empId.Value)
                : await _service.GetAllAsync();

            List<Employee> employees = await _empService.SearchAsync(null, null, EmployeeStatus.Active.ToValue());
            DependentIndexViewModel vm = new DependentIndexViewModel
            {
                Dependents  = dependents,
                FilterEmpId = empId,
                EmployeeOptions = employees
                    .Select(e => new SelectListItem($"{e.Code} - {e.FullName}", e.Id.ToString()))
                    .ToList()
            };
            vm.EmployeeOptions.Insert(0, new SelectListItem("-- Tất cả nhân viên --", ""));
            return View(vm);
        }
        else
        {
            if (CurrentEmployeeId == null) return Forbid();
            List<EmployeeDependent> dependents = await _service.GetByEmployeeIdAsync(CurrentEmployeeId.Value);
            return View(new DependentIndexViewModel { Dependents = dependents });
        }
    }

    [HttpPost("api/dependents")]
    [Authorize(Roles = "HR_MANAGER")]
    public async Task<IActionResult> Create([FromBody] DependentFormViewModel vm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { error = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) });
        string? error = await _service.CreateAsync(vm, CurrentUsername);
        return error != null ? BadRequest(new { error }) : Ok(new { success = true });
    }

    [HttpPut("api/dependents/{id:guid}")]
    [Authorize(Roles = "HR_MANAGER")]
    public async Task<IActionResult> Update(Guid id, [FromBody] DependentFormViewModel vm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { error = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) });
        string? error = await _service.UpdateAsync(id, vm, CurrentUsername);
        return error != null ? BadRequest(new { error }) : Ok(new { success = true });
    }

    [HttpPatch("api/dependents/{id:guid}/deactivate")]
    [Authorize(Roles = "HR_MANAGER")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        string? error = await _service.DeactivateAsync(id, CurrentUsername);
        return error != null ? BadRequest(new { error }) : Ok(new { success = true });
    }

    private async Task PopulateEmployeeOptions(DependentFormViewModel vm)
    {
        List<Employee> employees = await _empService.SearchAsync(null, null, EmployeeStatus.Active.ToValue());
        vm.EmployeeOptions = employees
            .Select(e => new SelectListItem($"{e.Code} - {e.FullName}", e.Id.ToString()))
            .ToList();
        vm.EmployeeOptions.Insert(0, new SelectListItem("-- Chọn nhân viên --", ""));
    }
}
