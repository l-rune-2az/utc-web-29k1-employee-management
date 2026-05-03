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
            // HR_MANAGER: xem tất cả hoặc lọc theo nhân viên
            List<EmployeeAllowance> allowances = empId.HasValue
                ? await _service.GetByEmployeeIdAsync(empId.Value)
                : await _service.GetAllAsync();

            List<Employee> employees = await _empService.SearchAsync(null, null, EmployeeStatus.Active.ToValue());
            List<Contract> contracts = await _contractService.GetAllAsync();
            List<AllowanceConfig> configs = await _allowanceConfigService.GetActiveAsync();

            EmployeeAllowanceIndexViewModel vm = new EmployeeAllowanceIndexViewModel
            {
                EmployeeAllowances = allowances,
                FilterEmpId = empId,
                EmployeeOptions = employees.Select(e => new SelectListItem($"{e.Code} - {e.FullName}", e.Id.ToString())).ToList(),
                ContractOptions  = contracts.Select(c => new SelectListItem(c.ContractNumber, c.Id.ToString())).ToList(),
                AllowanceOptions = configs.Select(a => new SelectListItem(a.Name, a.Id.ToString())).ToList()
            };
            vm.EmployeeOptions.Insert(0, new SelectListItem("-- Tất cả nhân viên --", ""));
            vm.ContractOptions.Insert(0, new SelectListItem("-- Chọn hợp đồng --", ""));
            vm.AllowanceOptions.Insert(0, new SelectListItem("-- Chọn loại phụ cấp --", ""));
            return View(vm);
        }
        else
        {
            // EMPLOYEE: chỉ xem phụ cấp của mình
            if (CurrentEmployeeId == null) return Forbid();

            List<EmployeeAllowance> allowances = await _service.GetByEmployeeIdAsync(CurrentEmployeeId.Value);
            return View(new EmployeeAllowanceIndexViewModel { EmployeeAllowances = allowances });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "HR_MANAGER")]
    public async Task<IActionResult> Create(EmployeeAllowanceFormViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return RedirectToAction(nameof(Index));
        }
        string? error = await _service.CreateAsync(vm, CurrentUsername);
        TempData[error != null ? "Error" : "Success"] = error ?? "Thêm phụ cấp thành công!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "HR_MANAGER")]
    public async Task<IActionResult> Edit(Guid id, EmployeeAllowanceFormViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return RedirectToAction(nameof(Index));
        }
        string? error = await _service.UpdateAsync(id, vm, CurrentUsername);
        TempData[error != null ? "Error" : "Success"] = error ?? "Cập nhật phụ cấp thành công!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "HR_MANAGER")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        string? error = await _service.DeactivateAsync(id, CurrentUsername);
        TempData[error != null ? "Error" : "Success"] = error ?? "Đã vô hiệu hóa phụ cấp!";
        return RedirectToAction(nameof(Index));
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
