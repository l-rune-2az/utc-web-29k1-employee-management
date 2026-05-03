using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Models.Entities;

namespace EmployeeManagement.Data;

// ApplicationDbContext là cầu nối giữa ứng dụng C# và database PostgreSQL
// Mỗi DbSet<T> tương ứng với một bảng trong database
public class ApplicationDbContext : DbContext
{
    // Constructor: ASP.NET tự inject options vào đây (Dependency Injection)
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Khai báo các bảng trong database
    // null! — EF Core tự gán giá trị khi khởi tạo, không bao giờ thực sự null khi dùng
    public DbSet<Department> Departments { get; set; } = null!;
    public DbSet<Position> Positions { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Users> Users { get; set; } = null!;
    public DbSet<Contract> Contracts { get; set; } = null!;
    public DbSet<EmployeeDependent> EmployeeDependents { get; set; } = null!;
    public DbSet<AllowanceConfig> AllowanceConfigs { get; set; } = null!;
    public DbSet<EmployeeAllowance> EmployeeAllowances { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --- DEPARTMENT ---
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasIndex(e => e.Code).IsUnique();

            // Quan hệ tự tham chiếu: phòng ban có thể có phòng ban cha
            entity.HasOne(e => e.Parent)
                  .WithMany(e => e.Children)
                  .HasForeignKey(e => e.ParentId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // --- POSITION ---
        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasIndex(e => e.Code).IsUnique();
        });

        // --- EMPLOYEE ---
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasIndex(e => e.Code).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.IdCard).IsUnique();

            // Không dùng FK trong DB — quan hệ được kiểm tra trong ứng dụng
            entity.HasOne(e => e.Department)
                  .WithMany(d => d.Employees)
                  .HasForeignKey(e => e.DeptId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Position)
                  .WithMany(p => p.Employees)
                  .HasForeignKey(e => e.PositionId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // --- USERS ---
        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasIndex(e => e.EmployeeId).IsUnique();

            entity.HasOne(u => u.Employee)
                  .WithOne(e => e.User)
                  .HasForeignKey<Users>(u => u.EmployeeId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // --- CONTRACT ---
        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasIndex(e => e.ContractNumber).IsUnique();

            // Một nhân viên không thể có 2 hợp đồng bắt đầu cùng ngày
            entity.HasIndex(e => new { e.EmpId, e.StartDate }).IsUnique();

            entity.HasOne(c => c.Employee)
                  .WithMany(e => e.Contracts)
                  .HasForeignKey(c => c.EmpId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // --- EMPLOYEE DEPENDENT ---
        modelBuilder.Entity<EmployeeDependent>(entity =>
        {
            entity.HasOne(d => d.Employee)
                  .WithMany(e => e.Dependents)
                  .HasForeignKey(d => d.EmpId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // --- ALLOWANCE CONFIG ---
        modelBuilder.Entity<AllowanceConfig>(entity =>
        {
            entity.HasIndex(e => e.Code).IsUnique();
        });

        // --- EMPLOYEE ALLOWANCE ---
        modelBuilder.Entity<EmployeeAllowance>(entity =>
        {
            entity.HasOne(a => a.Employee)
                  .WithMany(e => e.Allowances)
                  .HasForeignKey(a => a.EmpId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(a => a.Contract)
                  .WithMany(c => c.Allowances)
                  .HasForeignKey(a => a.ContractId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(a => a.AllowanceConfig)
                  .WithMany(ac => ac.EmployeeAllowances)
                  .HasForeignKey(a => a.AllowanceId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
