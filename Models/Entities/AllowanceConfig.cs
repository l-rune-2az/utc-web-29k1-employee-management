using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmployeeManagement.Models.Enums;

namespace EmployeeManagement.Models.Entities;

// Bảng danh mục loại phụ cấp (ăn trưa, điện thoại, xăng xe, v.v.)
[Table("allowance_config")]
public class AllowanceConfig
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
    public string Name { get; set; } = string.Empty;

    [Column("description")]
    public string? Description { get; set; }

    // Mức phụ cấp mặc định — null = không có mức cố định, thương lượng từng người
    [Column("default_amount", TypeName = "numeric(15,2)")]
    public decimal? DefaultAmount { get; set; }

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

    public ICollection<EmployeeAllowance> EmployeeAllowances { get; set; } = new List<EmployeeAllowance>();
}
