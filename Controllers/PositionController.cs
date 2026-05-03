using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.ViewModels;
using EmployeeManagement.Services;

namespace EmployeeManagement.Controllers;

[Authorize(Roles = "HR_MANAGER")]
public class PositionController : BaseController
{
    private readonly IPositionService _service;
    private readonly IEmployeeService _empService;

    public PositionController(IPositionService service, IEmployeeService empService)
    {
        _service = service;
        _empService = empService;
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        Position? pos = await _service.GetByIdAsync(id);
        if (pos == null) return NotFound();
        return View(new PositionFormViewModel
        {
            Id = pos.Id, Code = pos.Code, Name = pos.Name,
            Level = pos.Level, Description = pos.Description
        });
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        Position? pos = await _service.GetByIdAsync(id);
        if (pos == null) return NotFound();

        List<Employee> all = await _empService.SearchAsync(null, null, null);
        return View(new PositionDetailViewModel
        {
            Position  = pos,
            Employees = all.Where(e => e.PositionId == id).ToList()
        });
    }

    public async Task<IActionResult> Index(string? keyword, int page = 1)
    {
        const int pageSize = 10;
        List<Position> all = await _service.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            string kw = keyword.Trim().ToLower();
            all = all.Where(p => p.Name.ToLower().Contains(kw) || p.Code.ToLower().Contains(kw)).ToList();
        }

        int total = all.Count;
        return View(new PositionIndexViewModel
        {
            Positions  = all.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
            SearchKeyword = keyword, Page = page, PageSize = pageSize, TotalCount = total
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PositionFormViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return RedirectToAction(nameof(Index));
        }
        string? error = await _service.CreateAsync(vm, CurrentUsername);
        TempData[error != null ? "Error" : "Success"] = error ?? "Thêm chức vụ thành công!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, PositionFormViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return RedirectToAction(nameof(Index));
        }
        string? error = await _service.UpdateAsync(id, vm, CurrentUsername);
        TempData[error != null ? "Error" : "Success"] = error ?? "Cập nhật chức vụ thành công!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        string? error = await _service.DeactivateAsync(id, CurrentUsername);
        TempData[error != null ? "Error" : "Success"] = error ?? "Đã vô hiệu hóa chức vụ!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleStatus(Guid id)
    {
        await _service.ToggleStatusAsync(id, CurrentUsername);
        return RedirectToAction(nameof(Index));
    }
}