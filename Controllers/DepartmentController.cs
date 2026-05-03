using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.ViewModels;
using EmployeeManagement.Services;

namespace EmployeeManagement.Controllers;

// Chỉ HR_MANAGER mới được quản lý phòng ban
[Authorize(Roles = "HR_MANAGER")]
public class DepartmentController : BaseController
{
    private readonly IDepartmentService _service;
    private readonly IEmployeeService _empService;

    public DepartmentController(IDepartmentService service, IEmployeeService empService)
    {
        _service = service;
        _empService = empService;
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        Department? dept = await _service.GetByIdAsync(id);
        if (dept == null) return NotFound();
        DepartmentFormViewModel vm = new DepartmentFormViewModel
        {
            Id = dept.Id, Code = dept.Code, Name = dept.Name,
            Description = dept.Description, ParentId = dept.ParentId
        };
        await PopulateParentOptions(vm, id);
        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        Department? dept = await _service.GetByIdAsync(id);
        if (dept == null) return NotFound();

        List<Department> all = await _service.GetAllAsync();
        List<Employee> employees = await _empService.SearchAsync(null, id, null);

        return View(new DepartmentDetailViewModel
        {
            Department     = dept,
            SubDepartments = all.Where(d => d.ParentId == id).ToList(),
            Employees      = employees
        });
    }

    public async Task<IActionResult> Index(string? keyword, int page = 1)
    {
        const int pageSize = 10;
        List<Department> all = await _service.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            string kw = keyword.Trim().ToLower();
            all = all.Where(d => d.Name.ToLower().Contains(kw) || d.Code.ToLower().Contains(kw)).ToList();
        }

        int total = all.Count;
        List<Department> paged = all.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        List<SelectListItem> parentOpts = all
            .Select(d => new SelectListItem(d.Name, d.Id.ToString()))
            .ToList();
        parentOpts.Insert(0, new SelectListItem("-- Không có --", ""));

        DepartmentIndexViewModel vm = new DepartmentIndexViewModel
        {
            Departments   = paged,
            SearchKeyword = keyword,
            Page          = page,
            PageSize      = pageSize,
            TotalCount    = total,
            ParentOptions = parentOpts
        };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DepartmentFormViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return RedirectToAction(nameof(Index));
        }
        string? error = await _service.CreateAsync(vm, CurrentUsername);
        TempData[error != null ? "Error" : "Success"] = error ?? "Thêm phòng ban thành công!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, DepartmentFormViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return RedirectToAction(nameof(Index));
        }
        string? error = await _service.UpdateAsync(id, vm, CurrentUsername);
        TempData[error != null ? "Error" : "Success"] = error ?? "Cập nhật phòng ban thành công!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        string? error = await _service.DeactivateAsync(id, CurrentUsername);
        if (error != null) TempData["Error"] = error;
        else TempData["Success"] = "Đã vô hiệu hóa phòng ban!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleStatus(Guid id)
    {
        await _service.ToggleStatusAsync(id, CurrentUsername);
        return RedirectToAction(nameof(Index));
    }

    // Helper: điền danh sách phòng ban cha vào dropdown
    private async Task PopulateParentOptions(DepartmentFormViewModel vm, Guid? excludeId = null)
    {
        List<Department> departments = await _service.GetActiveAsync();
        // Loại trừ chính nó khỏi danh sách phòng ban cha (không cho phép tự tham chiếu)
        if (excludeId.HasValue)
            departments = departments.Where(d => d.Id != excludeId.Value).ToList();

        vm.ParentOptions = departments
            .Select(d => new SelectListItem(d.Name, d.Id.ToString()))
            .ToList();
        vm.ParentOptions.Insert(0, new SelectListItem("-- Không có (phòng ban gốc) --", ""));
    }
}