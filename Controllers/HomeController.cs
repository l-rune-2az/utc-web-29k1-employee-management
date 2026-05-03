using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models.Entities;
using EmployeeManagement.Models.Enums;
using EmployeeManagement.Models.ViewModels;
using EmployeeManagement.Services;

namespace EmployeeManagement.Controllers;

[Authorize]
public class HomeController : BaseController
{
    private readonly IEmployeeService _empService;
    private readonly IContractService _contractService;
    private readonly IDepartmentService _deptService;

    public HomeController(IEmployeeService empService, IContractService contractService, IDepartmentService deptService)
    {
        _empService = empService;
        _contractService = contractService;
        _deptService = deptService;
    }

    public async Task<IActionResult> Index()
    {
        if (!User.IsInRole("HR_MANAGER"))
            return View(new DashboardViewModel());

        List<Employee> allEmployees   = await _empService.SearchAsync(null, null, null);
        List<Contract> allContracts   = await _contractService.GetAllAsync();
        List<Department> allDepts     = await _deptService.GetAllAsync();

        DateOnly today     = DateOnly.FromDateTime(DateTime.UtcNow);
        DateOnly in30Days  = today.AddDays(30);

        // 6 tháng gần nhất (tháng 1 là tháng cũ nhất)
        var now    = DateTime.UtcNow;
        var months = Enumerable.Range(0, 6)
            .Select(i => new DateTime(now.Year, now.Month, 1).AddMonths(-5 + i))
            .ToList();

        var growthLabels  = months.Select(m => $"T{m.Month}/{m.Year % 100:D2}").ToList();

        // Chart 1: tổng headcount tích lũy cuối mỗi tháng
        var headcountData = months.Select(m =>
        {
            var endOfMonth = DateOnly.FromDateTime(m.AddMonths(1).AddDays(-1));
            return allEmployees.Count(e => e.HireDate.HasValue && e.HireDate.Value <= endOfMonth);
        }).ToList();

        // Chart 2: tuyển mới theo từng phòng ban trong tháng (top 6 phòng ban có nhân viên)
        var topDepts = allEmployees
            .Where(e => e.Department != null)
            .GroupBy(e => e.Department!.Name)
            .OrderByDescending(g => g.Count())
            .Take(6)
            .Select(g => g.Key)
            .ToList();

        var deptMonthlyHires = topDepts.Select(deptName =>
            months.Select(m =>
            {
                var start = DateOnly.FromDateTime(m);
                var end   = DateOnly.FromDateTime(m.AddMonths(1).AddDays(-1));
                return allEmployees.Count(e =>
                    e.Department?.Name == deptName &&
                    e.HireDate.HasValue &&
                    e.HireDate.Value >= start &&
                    e.HireDate.Value <= end);
            }).ToList()
        ).ToList();

        DashboardViewModel vm = new DashboardViewModel
        {
            TotalEmployees    = allEmployees.Count,
            ActiveEmployees   = allEmployees.Count(e => e.Status == EmployeeStatus.Active.ToValue()),
            InactiveEmployees = allEmployees.Count(e => e.Status == EmployeeStatus.Inactive.ToValue()),
            TotalDepartments  = allDepts.Count(d => d.Status == DepartmentStatus.Active.ToValue()),
            ExpiringContracts = allContracts.Count(c =>
                c.Status == ContractStatus.Active.ToValue() &&
                c.EndDate.HasValue && c.EndDate.Value <= in30Days),
            RecentEmployees   = allEmployees.OrderByDescending(e => e.CreatedAt).Take(5).ToList(),
            GrowthLabels      = growthLabels,
            HeadcountData     = headcountData,
            DeptNames         = topDepts,
            DeptMonthlyHires  = deptMonthlyHires
        };

        return View(vm);
    }
}
