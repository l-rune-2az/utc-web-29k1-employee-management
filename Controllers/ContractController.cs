using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.Enums;
using EmployeeManagement.Models.ViewModels;
using EmployeeManagement.Services;

namespace EmployeeManagement.Controllers;

[Authorize]
public class ContractController : BaseController
{
    private readonly IContractService _contractService;
    private readonly IEmployeeService _empService;
    private readonly IEmployeeAllowanceService _allowanceService;

    public ContractController(IContractService contractService, IEmployeeService empService, IEmployeeAllowanceService allowanceService)
    {
        _contractService = contractService;
        _empService = empService;
        _allowanceService = allowanceService;
    }

    [HttpGet]
    [Authorize(Roles = "HR_MANAGER")]
    public async Task<IActionResult> Edit(Guid id)
    {
        Contract? contract = await _contractService.GetByIdAsync(id);
        if (contract == null) return NotFound();
        ContractFormViewModel vm = new ContractFormViewModel
        {
            Id = contract.Id, EmpId = contract.EmpId,
            ContractNumber = contract.ContractNumber, ContractType = contract.ContractType,
            StartDate = contract.StartDate, EndDate = contract.EndDate,
            BaseSalary = contract.BaseSalary, OfferSalary = contract.OfferSalary,
            SalaryType = contract.SalaryType, Notes = contract.Notes
        };
        await PopulateEmployeeOptions(vm);
        return View(vm);
    }

    [HttpGet]
    [Authorize(Roles = "HR_MANAGER")]
    public async Task<IActionResult> Details(Guid id)
    {
        Contract? contract = await _contractService.GetByIdAsync(id);
        if (contract == null) return NotFound();

        List<EmployeeAllowance> allowances = await _allowanceService.GetByContractIdAsync(id);
        return View(new ContractDetailViewModel { Contract = contract, Allowances = allowances });
    }

    public async Task<IActionResult> Index(string? keyword, string? status, int page = 1)
    {
        const int pageSize = 10;
        List<Contract> all;

        if (User.IsInRole("HR_MANAGER"))
            all = await _contractService.GetAllAsync();
        else
        {
            if (CurrentEmployeeId == null) return Forbid();
            all = await _contractService.GetByEmployeeIdAsync(CurrentEmployeeId.Value);
        }

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            string kw = keyword.Trim().ToLower();
            all = all.Where(c =>
                (c.Employee?.FullName.ToLower().Contains(kw) ?? false) ||
                c.ContractNumber.ToLower().Contains(kw)).ToList();
        }

        if (!string.IsNullOrWhiteSpace(status))
            all = all.Where(c => c.Status == status).ToList();

        int total = all.Count;
        List<Employee> empList = User.IsInRole("HR_MANAGER")
            ? await _empService.SearchAsync(null, null, EmployeeStatus.Active.ToValue())
            : new();
        List<SelectListItem> empOptions = empList
            .Select(e => new SelectListItem($"{e.Code} - {e.FullName}", e.Id.ToString()))
            .ToList();
        empOptions.Insert(0, new SelectListItem("-- Chọn nhân viên --", ""));

        return View(new ContractIndexViewModel
        {
            Contracts       = all.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
            SearchKeyword   = keyword, FilterStatus = status,
            Page = page, PageSize = pageSize, TotalCount = total,
            EmployeeOptions = empOptions
        });
    }

    [HttpPost("api/contracts")]
    [Authorize(Roles = "HR_MANAGER")]
    public async Task<IActionResult> Create([FromBody] ContractFormViewModel vm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { error = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) });
        string? error = await _contractService.CreateAsync(vm, CurrentUsername);
        return error != null ? BadRequest(new { error }) : Ok(new { success = true });
    }

    [HttpPut("api/contracts/{id:guid}")]
    [Authorize(Roles = "HR_MANAGER")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ContractFormViewModel vm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { error = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) });
        string? error = await _contractService.UpdateAsync(id, vm, CurrentUsername);
        return error != null ? BadRequest(new { error }) : Ok(new { success = true });
    }

    [HttpPatch("api/contracts/{id:guid}/terminate")]
    [Authorize(Roles = "HR_MANAGER")]
    public async Task<IActionResult> Terminate(Guid id)
    {
        string? error = await _contractService.TerminateAsync(id, CurrentUsername);
        return error != null ? BadRequest(new { error }) : Ok(new { success = true });
    }

    private async Task PopulateEmployeeOptions(ContractFormViewModel vm)
    {
        List<Employee> employees = await _empService.SearchAsync(null, null, EmployeeStatus.Active.ToValue());
        vm.EmployeeOptions = employees
            .Select(e => new SelectListItem($"{e.Code} - {e.FullName}", e.Id.ToString()))
            .ToList();
        vm.EmployeeOptions.Insert(0, new SelectListItem("-- Chọn nhân viên --", ""));
    }
}
