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
            Positions     = all.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
            SearchKeyword = keyword, Page = page, PageSize = pageSize, TotalCount = total
        });
    }

    [HttpPost("api/positions")]
    public async Task<IActionResult> Create([FromBody] PositionFormViewModel vm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { error = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) });
        string? error = await _service.CreateAsync(vm, CurrentUsername);
        return error != null ? BadRequest(new { error }) : Ok(new { success = true });
    }

    [HttpPut("api/positions/{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] PositionFormViewModel vm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { error = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) });
        string? error = await _service.UpdateAsync(id, vm, CurrentUsername);
        return error != null ? BadRequest(new { error }) : Ok(new { success = true });
    }

    [HttpPatch("api/positions/{id:guid}/toggle")]
    public async Task<IActionResult> Toggle(Guid id)
    {
        string? error = await _service.ToggleStatusAsync(id, CurrentUsername);
        return error != null ? BadRequest(new { error }) : Ok(new { success = true });
    }
}
