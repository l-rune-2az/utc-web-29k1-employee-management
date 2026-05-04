using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmployeeManagement.Models.Enums;

namespace EmployeeManagement.Models.Entities;

// Bảng phụ cấp thực tế của nhân viên — gắn với từng hợp đồng
[Table("employee_allowance")]
public class EmployeeAllowance
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [Column("emp_id")]
    public Guid EmpId { get; set; }

    [Required]
    [Column("contract_id")]
    public Guid ContractId { get; set; }

    // Loại phụ cấp được áp dụng
    [Required]
    [Column("allowance_id")]
    public Guid AllowanceId { get; set; }

    // Số tiền thực tế (có thể khác default_amount trong allowance_config)
    [Required]
    [Column("amount", TypeName = "numeric(15,2)")]
    public decimal Amount { get; set; }

    // Ngày bắt đầu áp dụng phụ cấp
    [Column("effective_date")]
    public DateOnly? EffectiveDate { get; set; }

    [Column("end_date")]
    public DateOnly? EndDate { get; set; }

    [MaxLength(10)]
    [Column("status")]
    public string Status { get; set; } = AllowanceStatus.Active.ToValue();

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

    [ForeignKey("ContractId")]
    public Contract? Contract { get; set; }

    [ForeignKey("AllowanceId")]
    public AllowanceConfig? AllowanceConfig { get; set; }
}
