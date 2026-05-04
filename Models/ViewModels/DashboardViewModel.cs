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

    
    public List<string> GrowthLabels    { get; set; } = new();
    public List<int>    HeadcountData   { get; set; } = new();

    
    public List<string>       DeptNames        { get; set; } = new();
    public List<List<int>>    DeptMonthlyHires { get; set; } = new();
}
