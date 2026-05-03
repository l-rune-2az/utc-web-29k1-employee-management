using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.Enums;
using EmployeeManagement.Models.ViewModels;
using EmployeeManagement.Services;

namespace EmployeeManagement.Controllers;

[Authorize]
public class EmployeeAllowanceController : BaseController
{
    private readonly IEmployeeAllowanceService _service;
    private readonly IEmployeeService _empService;
    private readonly IContractService _contractService;
    private readonly IAllowanceConfigService _allowanceConfigService;

    public EmployeeAllowanceController(
        IEmployeeAllowanceService service,
        IEmployeeService empService,
        IContractService contractService,
        IAllowanceConfigService allowanceConfigService)
    {
        _service = service;
        _empService = empService;
        _contractService = contractService;
        _allowanceConfigService = allowanceConfigService;
    }

    [HttpGet]
    [Authorize(Roles = "HR_MANAGER")]
    public async Task<IActionResult> Edit(Guid id)
    {
        EmployeeAllowance? ea = await _service.GetByIdAsync(id);
        if (ea == null) return NotFound();
        EmployeeAllowanceFormViewModel vm = new EmployeeAllowanceFormViewModel
        {
            Id = ea.Id, EmpId = ea.EmpId, ContractId = ea.ContractId,
            AllowanceId = ea.AllowanceId, Amount = ea.Amount, EffectiveDate = ea.EffectiveDate
        };
        await PopulateDropdowns(vm);
        return View(vm);
    }

    public async Task<IActionResult> Index(Guid? empId)
    {
        if (User.IsInRole("HR_MANAGER"))
        {
            List<EmployeeAllowance> allowances = empId.HasValue
                ? await _service.GetByEmployeeIdAsync(empId.Value)
                : await _service.GetAllAsync();

            List<Employee> employees = await _empService.SearchAsync(null, null, EmployeeStatus.Active.ToValue());
            List<Contract> contracts = await _contractService.GetAllAsync();
            List<AllowanceConfig> configs = await _allowanceConfigService.GetActiveAsync();

            EmployeeAllowanceIndexViewModel vm = new EmployeeAllowanceIndexViewModel
            {
                EmployeeAllowances = allowances,
                FilterEmpId        = empId,
                EmployeeOptions    = employees.Select(e => new SelectListItem($"{e.Code} - {e.FullName}", e.Id.ToString())).ToList(),
                ContractOptions    = contracts.Select(c => new SelectListItem(c.ContractNumber, c.Id.ToString())).ToList(),
                AllowanceOptions   = configs.Select(a => new SelectListItem(a.Name, a.Id.ToString())).ToList()
            };
            vm.EmployeeOptions.Insert(0, new SelectListItem("-- Tất cả nhân viên --", ""));
            vm.ContractOptions.Insert(0, new SelectListItem("-- Chọn hợp đồng --", ""));
            vm.AllowanceOptions.Insert(0, new SelectListItem("-- Chọn loại phụ cấp --", ""));
            return View(vm);
        }
        else
        {
            if (CurrentEmployeeId == null) return Forbid();
            List<EmployeeAllowance> allowances = await _service.GetByEmployeeIdAsync(CurrentEmployeeId.Value);
            return View(new EmployeeAllowanceIndexViewModel { EmployeeAllowances = allowances });
        }
    }

    [HttpPost("api/employee-allowances")]
    [Authorize(Roles = "HR_MANAGER")]
    public async Task<IActionResult> Create([FromBody] EmployeeAllowanceFormViewModel vm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { error = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) });
        string? error = await _service.CreateAsync(vm, CurrentUsername);
        return error != null ? BadRequest(new { error }) : Ok(new { success = true });
    }

    [HttpPut("api/employee-allowances/{id:guid}")]
    [Authorize(Roles = "HR_MANAGER")]
    public async Task<IActionResult> Update(Guid id, [FromBody] EmployeeAllowanceFormViewModel vm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { error = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) });
        string? error = await _service.UpdateAsync(id, vm, CurrentUsername);
        return error != null ? BadRequest(new { error }) : Ok(new { success = true });
    }

    [HttpPatch("api/employee-allowances/{id:guid}/deactivate")]
    [Authorize(Roles = "HR_MANAGER")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        string? error = await _service.DeactivateAsync(id, CurrentUsername);
        return error != null ? BadRequest(new { error }) : Ok(new { success = true });
    }

    private async Task PopulateDropdowns(EmployeeAllowanceFormViewModel vm)
    {
        List<Employee> employees = await _empService.SearchAsync(null, null, EmployeeStatus.Active.ToValue());
        vm.EmployeeOptions = employees
            .Select(e => new SelectListItem($"{e.Code} - {e.FullName}", e.Id.ToString()))
            .ToList();
        vm.EmployeeOptions.Insert(0, new SelectListItem("-- Chọn nhân viên --", ""));

        List<Contract> contracts = await _contractService.GetAllAsync();
        vm.ContractOptions = contracts
            .Select(c => new SelectListItem(c.ContractNumber, c.Id.ToString()))
            .ToList();
        vm.ContractOptions.Insert(0, new SelectListItem("-- Chọn hợp đồng --", ""));

        List<AllowanceConfig> configs = await _allowanceConfigService.GetActiveAsync();
        vm.AllowanceOptions = configs
            .Select(a => new SelectListItem(a.Name, a.Id.ToString()))
            .ToList();
        vm.AllowanceOptions.Insert(0, new SelectListItem("-- Chọn loại phụ cấp --", ""));
    }
}
