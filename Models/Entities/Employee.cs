using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmployeeManagement.Models.Enums;

namespace EmployeeManagement.Models.Entities;

[Table("employee")]
public class Employee
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    
    [Required]
    [MaxLength(20)]
    [Column("code")]
    public string Code { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [Column("name")]
    public string FullName { get; set; } = string.Empty;

    
    [MaxLength(10)]
    [Column("gender")]
    public string? Gender { get; set; }

    [Column("dob")]
    public DateOnly? DateOfBirth { get; set; }

    
    [MaxLength(20)]
    [Column("id_card")]
    public string? IdCard { get; set; }

    [Required]
    [MaxLength(150)]
    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [MaxLength(20)]
    [Column("phone")]
    public string? Phone { get; set; }

    [Column("address")]
    public string? Address { get; set; }

    [Column("hire_date")]
    public DateOnly? HireDate { get; set; }

    
    [Column("dept_id")]
    public Guid? DeptId { get; set; }

    
    [Column("position_id")]
    public Guid? PositionId { get; set; }

    [MaxLength(10)]
    [Column("status")]
    public string Status { get; set; } = EmployeeStatus.Active.ToValue();

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [MaxLength(100)]
    [Column("created_by")]
    public string? CreatedBy { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [MaxLength(100)]
    [Column("updated_by")]
    public string? UpdatedBy { get; set; }

    
    [ForeignKey("DeptId")]
    public Department? Department { get; set; }

    [ForeignKey("PositionId")]
    public Position? Position { get; set; }

    public Users? User { get; set; }
    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    public ICollection<EmployeeDependent> Dependents { get; set; } = new List<EmployeeDependent>();
    public ICollection<EmployeeAllowance> Allowances { get; set; } = new List<EmployeeAllowance>();
}
