using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmployeeManagement.Models.Enums;

namespace EmployeeManagement.Models.Entities;

[Table("employee_dependent")]
public class EmployeeDependent
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [Column("emp_id")]
    public Guid EmpId { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("name")]
    public string FullName { get; set; } = string.Empty;

    [Column("dob")]
    public DateOnly? DateOfBirth { get; set; }

    [MaxLength(20)]
    [Column("relationship")]
    public string? Relationship { get; set; }

    [MaxLength(20)]
    [Column("id_card")]
    public string? NationalId { get; set; }

    [MaxLength(10)]
    [Column("status")]
    public string Status { get; set; } = DependentStatus.Active.ToValue();

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

    [ForeignKey("EmpId")]
    public Employee? Employee { get; set; }
}
