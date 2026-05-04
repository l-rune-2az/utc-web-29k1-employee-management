using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmployeeManagement.Models.Enums;

namespace EmployeeManagement.Models.Entities;

[Table("contract")]
public class Contract
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [Column("emp_id")]
    public Guid EmpId { get; set; }

    
    [Required]
    [MaxLength(50)]
    [Column("contract_number")]
    public string ContractNumber { get; set; } = string.Empty;

    
    [MaxLength(20)]
    [Column("contract_type")]
    public string ContractType { get; set; } = Models.Enums.ContractType.Official.ToValue();

    [Required]
    [Column("start_date")]
    public DateOnly StartDate { get; set; }

    
    [Column("end_date")]
    public DateOnly? EndDate { get; set; }

    
    [Required]
    [Column("base_salary", TypeName = "numeric(15,2)")]
    public decimal BaseSalary { get; set; }

    
    [Required]
    [Column("offer_salary", TypeName = "numeric(15,2)")]
    public decimal OfferSalary { get; set; }

    
    [MaxLength(10)]
    [Column("salary_type")]
    public string SalaryType { get; set; } = Models.Enums.SalaryType.Gross.ToValue();

    
    [MaxLength(20)]
    [Column("status")]
    public string Status { get; set; } = ContractStatus.Active.ToValue();

    [Column("terms")]
    public string? Notes { get; set; }

    [MaxLength(500)]
    [Column("file_url")]
    public string? FileUrl { get; set; }

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

    public ICollection<EmployeeAllowance> Allowances { get; set; } = new List<EmployeeAllowance>();
}
