using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.ViewModels;
using EmployeeManagement.Services;

namespace EmployeeManagement.Controllers;

[Authorize(Roles = "HR_MANAGER")]
public class EmployeeController : BaseController
{
    private readonly IEmployeeService _empService;
    private readonly IDepartmentService _deptService;
    private readonly IPositionService _posService;
    private readonly IContractService _contractService;
    private readonly IDependentService _dependentService;
    private readonly IEmployeeAllowanceService _allowanceService;

    public EmployeeController(
        IEmployeeService empService,
        IDepartmentService deptService,
        IPositionService posService,
        IContractService contractService,
        IDependentService dependentService,
        IEmployeeAllowanceService allowanceService)
    {
        _empService = empService;
        _deptService = deptService;
        _posService = posService;
        _contractService = contractService;
        _dependentService = dependentService;
        _allowanceService = allowanceService;
    }

    public async Task<IActionResult> Index(string? keyword, Guid? deptId, string? status, int page = 1)
    {
        const int pageSize = 10;
        List<Employee> all = await _empService.SearchAsync(keyword, deptId, status);
        List<Department> departments = await _deptService.GetActiveAsync();
        List<Position> positions = await _posService.GetActiveAsync();
        int total = all.Count;

        EmployeeIndexViewModel vm = new EmployeeIndexViewModel
        {
            Employees         = all.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
            SearchKeyword     = keyword,
            FilterDeptId      = deptId,
            FilterStatus      = status,
            Page              = page,
            PageSize          = pageSize,
            TotalCount        = total,
            DepartmentOptions = departments.Select(d => new SelectListItem(d.Name, d.Id.ToString())).ToList(),
            PositionOptions   = positions.Select(p => new SelectListItem(p.Name, p.Id.ToString())).ToList()
        };
        vm.DepartmentOptions.Insert(0, new SelectListItem("-- Tất cả phòng ban --", ""));
        vm.PositionOptions.Insert(0, new SelectListItem("-- Chọn chức vụ --", ""));
        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        Employee? emp = await _empService.GetByIdAsync(id);
        if (emp == null) return NotFound();
        EmployeeFormViewModel vm = new EmployeeFormViewModel
        {
            Id = emp.Id, FullName = emp.FullName, Gender = emp.Gender ?? "MALE",
            DateOfBirth = emp.DateOfBirth, IdCard = emp.IdCard,
            Email = emp.Email, Phone = emp.Phone, Address = emp.Address,
            HireDate = emp.HireDate, DeptId = emp.DeptId, PositionId = emp.PositionId
        };
        await PopulateDropdowns(vm);
        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        Employee? emp = await _empService.GetByIdAsync(id);
        if (emp == null) return NotFound();

        List<Contract> contracts           = await _contractService.GetByEmployeeIdAsync(id);
        List<EmployeeDependent> dependents = await _dependentService.GetByEmployeeIdAsync(id);
        List<EmployeeAllowance> allowances = await _allowanceService.GetByEmployeeIdAsync(id);

        return View(new EmployeeDetailViewModel
        {
            Employee   = emp,
            Contracts  = contracts,
            Dependents = dependents,
            Allowances = allowances
        });
    }

    [HttpPost("api/employees")]
    public async Task<IActionResult> Create([FromBody] EmployeeFormViewModel vm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { error = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) });
        string? error = await _empService.CreateAsync(vm, CurrentUsername);
        return error != null ? BadRequest(new { error }) : Ok(new { success = true });
    }

    [HttpPut("api/employees/{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] EmployeeFormViewModel vm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { error = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) });
        string? error = await _empService.UpdateAsync(id, vm, CurrentUsername);
        return error != null ? BadRequest(new { error }) : Ok(new { success = true });
    }

    [HttpPatch("api/employees/{id:guid}/toggle")]
    public async Task<IActionResult> Toggle(Guid id)
    {
        string? error = await _empService.ToggleStatusAsync(id, CurrentUsername);
        return error != null ? BadRequest(new { error }) : Ok(new { success = true });
    }

    private async Task PopulateDropdowns(EmployeeFormViewModel vm)
    {
        List<Department> departments = await _deptService.GetActiveAsync();
        List<Position> positions = await _posService.GetActiveAsync();

        vm.DepartmentOptions = departments
            .Select(d => new SelectListItem(d.Name, d.Id.ToString()))
            .ToList();
        vm.DepartmentOptions.Insert(0, new SelectListItem("-- Chọn phòng ban --", ""));

        vm.PositionOptions = positions
            .Select(p => new SelectListItem(p.Name, p.Id.ToString()))
            .ToList();
        vm.PositionOptions.Insert(0, new SelectListItem("-- Chọn chức vụ --", ""));
    }
}
