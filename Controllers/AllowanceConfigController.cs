using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models.ViewModels;
using EmployeeManagement.Models.Entities;
using EmployeeManagement.Services;

namespace EmployeeManagement.Controllers;

[Authorize(Roles = "HR_MANAGER")]
public class AllowanceConfigController : BaseController
{
    private readonly IAllowanceConfigService _service;
    private readonly IEmployeeAllowanceService _usageService;

    public AllowanceConfigController(IAllowanceConfigService service, IEmployeeAllowanceService usageService)
    {
        _service = service;
        _usageService = usageService;
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        AllowanceConfig? cfg = await _service.GetByIdAsync(id);
        if (cfg == null) return NotFound();
        return View(new AllowanceConfigFormViewModel
        {
            Id = cfg.Id, Code = cfg.Code, Name = cfg.Name,
            DefaultAmount = cfg.DefaultAmount, Description = cfg.Description
        });
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        AllowanceConfig? config = await _service.GetByIdAsync(id);
        if (config == null) return NotFound();

        List<EmployeeAllowance> all = await _usageService.GetAllAsync();
        return View(new AllowanceConfigDetailViewModel
        {
            Config = config,
            Usages = all.Where(a => a.AllowanceId == id).ToList()
        });
    }

    public async Task<IActionResult> Index(string? keyword, int page = 1)
    {
        const int pageSize = 10;
        List<AllowanceConfig> all = await _service.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            string kw = keyword.Trim().ToLower();
            all = all.Where(c => c.Name.ToLower().Contains(kw) || c.Code.ToLower().Contains(kw)).ToList();
        }

        int total = all.Count;
        return View(new AllowanceConfigIndexViewModel
        {
            AllowanceConfigs = all.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
            SearchKeyword = keyword, Page = page, PageSize = pageSize, TotalCount = total
        });
    }

    [HttpPost("api/allowance-configs")]
    public async Task<IActionResult> Create([FromBody] AllowanceConfigFormViewModel vm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { error = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) });
        string? error = await _service.CreateAsync(vm, CurrentUsername);
        return error != null ? BadRequest(new { error }) : Ok(new { success = true });
    }

    [HttpPut("api/allowance-configs/{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] AllowanceConfigFormViewModel vm)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { error = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) });
        string? error = await _service.UpdateAsync(id, vm, CurrentUsername);
        return error != null ? BadRequest(new { error }) : Ok(new { success = true });
    }

    [HttpPatch("api/allowance-configs/{id:guid}/toggle")]
    public async Task<IActionResult> Toggle(Guid id)
    {
        string? error = await _service.ToggleStatusAsync(id, CurrentUsername);
        return error != null ? BadRequest(new { error }) : Ok(new { success = true });
    }
}
