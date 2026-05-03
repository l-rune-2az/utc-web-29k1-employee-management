using EmployeeManagement.Models.Entities;

namespace EmployeeManagement.Models.ViewModels;

public class DashboardViewModel
{
    public int TotalEmployees    { get; set; }
    public int ActiveEmployees   { get; set; }
    public int InactiveEmployees { get; set; }
    public int ExpiringContracts { get; set; }
    public int TotalDepartments  { get; set; }
    public List<Employee> RecentEmployees { get; set; } = new();

    // Chart 1 — headcount tích lũy 6 tháng gần nhất
    public List<string> GrowthLabels    { get; set; } = new();
    public List<int>    HeadcountData   { get; set; } = new();

    // Chart 2 — tuyển mới theo phòng ban, 6 tháng gần nhất (stacked bar)
    public List<string>       DeptNames        { get; set; } = new();
    public List<List<int>>    DeptMonthlyHires { get; set; } = new();
}
